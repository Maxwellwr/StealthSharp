#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MockDefaults.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Linq;
using StealthSharp.Enumeration;
using StealthSharp.Model;

#endregion

namespace StealthSharp.MockServer
{
    /// <summary>Canned responses registered on every new <see cref="MockStealthServer" />.</summary>
    public static class MockDefaults
    {
        public const string ProfileName = "MockProfile";

        internal static void Register(MockStealthServer server)
        {
            // Commands the client does not wait for: accepted, no response.
            server.On(PacketType.SCLangVersion, _ => null);
            server.On(PacketType.SCSetEventProc, _ => null);

            server.RespondWith(PacketType.SCGetProfileName, ProfileName);
            server.RespondWith(PacketType.SCGetStealthInfo, new AboutData
            {
                StealthVersion = new ushort[] { 9, 6, 1 },
                Build = 1,
                BuildDate = new DateTime(2024, 1, 1),
                GitRevNumber = 1,
                GitRevision = "mock"
            });
            server.RespondWith(PacketType.SCGetGumpInfo, CreateGump(50));
        }

        /// <summary>Builds a gump with <paramref name="elements" /> buttons and texts, to get a large response.</summary>
        public static GumpInfo CreateGump(int elements)
        {
            return new GumpInfo
            {
                Serial = 1,
                GumpId = 2,
                X = 100,
                Y = 100,
                Pages = 1,
                ExtData = new ExtGumpInfo
                {
                    GumpButtons = Enumerable.Range(0, elements)
                        .Select(i => new GumpButton { X = i, Y = i, ReleasedId = i, PressedId = i, ReturnValue = i, ElemNum = i })
                        .ToArray(),
                    GumpText = Enumerable.Range(0, elements)
                        .Select(i => new GumpText { X = i, Y = i, TextId = i, ElemNum = i })
                        .ToArray()
                }
            };
        }
    }
}
