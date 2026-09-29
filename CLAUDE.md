# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

StealthSharp is a C#/.NET 7 wrapper (NuGet packages) for scripting the Ultima Online client via the Stealth TCP protocol. All source lives under `src/` (solution: `src/StealthSharp.sln`, SDK pinned by `src/global.json`).

## Commands

Run from `src/`:

```shell
dotnet restore
dotnet build -c Release
dotnet test --filter Category=Unit            # what CI runs
dotnet test --filter "FullyQualifiedName~SerializationTest"   # single test class / name
dotnet run -c Release --project StealthSharp.Benchmark        # BenchmarkDotNet (legacy default: needs a real Stealth)
dotnet run -c Release --project StealthSharp.Benchmark -- --filter '*MockNetwork*' --inProcess   # against the mock server
```

- Projects target `net7.0`. If only a newer SDK is installed (e.g. `apt-get install dotnet-sdk-8.0` in the cloud container), set `DOTNET_ROLL_FORWARD=Major` so tests and the console apps run on the newer runtime.
- Unit tests are tagged `[Trait("Category", "Unit")]`. Tests under `test/StealthSharp.Tests/Integration` need a running Stealth client and are excluded from CI — don't run them unfiltered.
- CI (`.github/workflows/nuget.yml`) runs on release publish/manual dispatch: build with `-p:Version=<tag>`, unit tests, pack, push to NuGet. There is no separate lint step.
- `TestScript` is a scratch console app for manual runs against a live Stealth client.

## Architecture

Four packages with a layered dependency: `StealthSharp` → `StealthSharp.Network` → `StealthSharp.Serialization` → `StealthSharp.Abstract`.

- **Abstract**: no logic — service interfaces (`Services/I*Service.cs`), models, enums, events, and the serialization/network contracts (`IMarshaler`, `IStealthSharpClient`, `SerializableAttribute`, `EventDataTypeAttribute`). Namespaces are flattened (`StealthSharp.Model`, `StealthSharp.Enumeration`, `StealthSharp.Event`, `StealthSharp.Services`), not folder-mirrored with the project name.
- **Serialization**: `Marshaler` is a reflection-based binary (de)serializer for the Stealth wire format (little-endian by default; `[Serializable(Endianness)]` on classes/structs). Property order in a model defines wire order. `ReflectionCache` caches metadata; special types use `ICustomConverter<T>` (`Converters/`, e.g. `DateTimeConverter`, `ServerEventDataConverter`) resolved through `CustomConverterFactory` via the service provider. `Marshaler` caches everything derivable from a `Type` in a private `TypeInfo` (kind, fixed size, converter, factories) and uses compiled property accessors (`PacketProperty`); keep hot paths free of per-call reflection. `Unit/WireFormatTest.cs` pins the exact bytes of representative models — any serializer change must keep it green, and `MarshalerBenchmark` (`-- --filter '*MarshalerBenchmark*' --inProcess`) measures it.
- **Network**: `StealthSharpClient` owns a `TcpClient` plus `System.IO.Pipelines`. Each request is a `PacketHeader` (`PacketType` + length) followed by a 2‑byte correlation id and a serialized body. Responses are matched back to callers by correlation id through a `WaitingDictionary`; unsolicited server events are pushed to `IObserver<ServerEventData>` subscribers.
- **StealthSharp** (main): `Stealth` is the facade. Base services are properties on it; the rest are fetched with `GetStealthService<T>()`. `ServiceProviderExtensions.AddStealthSharp()` wires everything into `IServiceCollection` (string/array length prefixes are configured as `uint` there). `EventSystemService` is a singleton; all other services are transient. `InternalService` handles connect handshake.

### Mock server

`StealthSharp.MockServer` (`MockStealthServer`) is an in-process TCP fake of Stealth, so the real client stack can be tested and benchmarked without a Windows Stealth/UO client. Its wire format is reverse engineered from the client code (see the class remarks), not from official docs. Register canned responses with `RespondWith(PacketType, value)` / `On(PacketType, handler)`; requests with no handler are counted in `UnhandledRequests` and never answered (the client would hang). `Unit/MockServerTest.cs` shows end-to-end usage (point `StealthOptions.Port` at `server.DiscoveryPort`).

### Adding or changing a Stealth command

Services derive from `BaseService` and are thin wrappers: they call `Client.SendPacketAsync<TResult>(PacketType.SC..., args)` and return the deserialized result. To add one: add the `PacketType` enum value (values must match the Stealth protocol numbering), add the method to the interface in `Abstract/Services`, implement it in `StealthSharp/Services`, and register any new service in `AddServices()`. New event payloads need an `EventType` member decorated with `[EventDataType(typeof(...))]` and a `[Serializable]` class in `Abstract/Event`.

## Conventions

- Files start with the `#region Copyright` MIT header block and group usings inside `#region` / `#endregion`.
- Library code uses `.ConfigureAwait(false)`; methods are `...Async` and return `Task`/`Task<T>`.
- `TelegramService` / `ViberService` exist but their DI registrations are commented out.
