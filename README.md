# Stealth Sharp

The wrapper is intended for writing scripts for the popular Ultima Online client - Stealth. Consists of 4 packages.

[![Stealth Sharp](https://github.com/Maxwellwr/StealthSharp/actions/workflows/nuget.yml/badge.svg)](https://github.com/Maxwellwr/StealthSharp/actions/workflows/nuget.yml)

| Package                    | Description                                  | Status                                                                                                                                          |
|----------------------------|----------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------|
| StealthSharp               | Main package                                 | [![Nuget](https://img.shields.io/nuget/v/StealthSharp?style=plastic)](https://www.nuget.org/packages/StealthSharp/)                             |
| StealthSharp.Abstract      | A collection of interfaces, models and enums | [![Nuget](https://img.shields.io/nuget/v/StealthSharp.Abstract?style=plastic)](https://www.nuget.org/packages/StealthSharp.Abstract/)           |
| StealthSharp.Network       | Networking                                   | [![Nuget](https://img.shields.io/nuget/v/StealthSharp.Network?style=plastic)](https://www.nuget.org/packages/StealthSharp.Network/)             |
| StealthSharp.Serialization | Implementing Stealth serialization           | [![Nuget](https://img.shields.io/nuget/v/StealthSharp.Serialization?style=plastic)](https://www.nuget.org/packages/StealthSharp.Serialization/) |

# Installation

```shell
dotnet add package StealthSharp
```

# Usage

Simple usage with default settings

```c#
IServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.AddStealthSharp();
var provider = serviceCollection.BuildServiceProvider();

var stealth = provider.GetRequiredService<Stealth>();
await stealth.ConnectToStealthAsync();
```

For configuration you can use the following code

```c#
serviceCollection.Configure<StealthOptions>(opt =>
    {
        opt.Host = "127.0.0.1";
        opt.Port = 47602;
    })
```

All Stealth functionality is divided into the following groups

Base - Available directly from the object Stealth

| Group             | Access             | Context                              |
|-------------------|--------------------|--------------------------------------|
| Connection        | stealth.Connection | Game server connection management    |
| Client            | stealth.Client     | Game client window management        |
| Journal           | stealth.Journal    | Working with the journal             |
| Search            | stealth.Search     | Search game objects                  |
| Game Object       | stealth.GameObject | Interact with objects                |
| Move Item         | stealth.MoveItem   | Various item movement                |
| Move Character    | stealth.Move       | Moving a character around the world  |
| Targets           | stealth.Target     | Target management                    |
| Character         | stealth.Char       | Character stats                      |
| Attack            | stealth.Attack     | War management                       |
| Dress layers      | stealth.Layer      | Dress and equip                      |
| Menu              | stealth.Menu       | Working with menu                    |
| Gumps             | stealth.Gump       | Working with gump                    |
| Skills and Spells | stealth.SkillSpell | Using and managing skills and spells |

Advanced - Available through `stealth.GetStealthService<T>()`

| Group               | Access                                             | Context                   |
|---------------------|----------------------------------------------------|---------------------------|
| Event System        | `stealth.GetStealthService<IEventSystemService>()` | Event management          |
| Context Menu        | `stealth.GetStealthService<IContextMenuService>()` | Working with context menu |
| Gesture             | `stealth.GetStealthService<IGestureService>()`     | Gestures                  |
| Global Chat         | `stealth.GetStealthService<IGlobalChatService>()`  | Global chat               |
| Global Stealth Vars | `stealth.GetStealthService<IGlobalService>()`      | Managing global variables |
| Info window         | `stealth.GetStealthService<IInfoWindowService>()`  | Using Stealth info window |
| Map                 | `stealth.GetStealthService<IMapService>()`         | Draw on map               |
| Market              | `stealth.GetStealthService<IMarketService>()`      | AutoBuy and AutoSell      |
| Party               | `stealth.GetStealthService<IPartyService>()`       | Party management          |
| Reagents            | `stealth.GetStealthService<IReagentService>()`     | Count regs in Backpack    |
| Stealth             | `stealth.GetStealthService<IStealthService>()`     | Stealth management        |
| Tiles               | `stealth.GetStealthService<ITileService>()`        | Tiles                     |
| Trade               | `stealth.GetStealthService<ITradeService>()`       | Trading                   |
