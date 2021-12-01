#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="EventDataGenerator.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

using System.Collections;
using System.Collections.Generic;
using StealthSharp.Enumeration;
using StealthSharp.Event;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp.Tests.DataGenerators
{
    public class EventDataGenerator : IEnumerable<object[]>
    {
        private List<object[]> _data = new()
        {
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 9
                },
                new ServerEventData<Identity>(EventType.ItemInfo, new Identity { Id = 231 }),
                "090000000300000101E7000000"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 9
                },
                new ServerEventData<Identity>(EventType.ItemDeleted, new Identity { Id = 231 }),
                "090000000300010101E7000000"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 53
                },
                new ServerEventData<SpeechEvent>(EventType.Speech, new SpeechEvent
                {
                    Sender = new Identity { Id = 231 },
                    Text = "Simple text",
                    SenderName = "Sender"
                }),
                "35000000030002030016000000530069006D0070006C00650020007400650078007400000C000000530065006E0064006500720001E7000000"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 18
                },
                new ServerEventData<MoveRejectionEvent>(EventType.MoveRejection,
                    new MoveRejectionEvent
                    {
                        Source = new WorldPoint { X = 123, Y = 321 },
                        Direction = Direction.East,
                        Destination = new WorldPoint { X = 567, Y = 765 }
                    }),
                "1200000003000405037B00034101060203370203FD02"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 12
                },
                new ServerEventData<DrawContainerEvent>(EventType.DrawContainer,
                    new DrawContainerEvent
                    {
                        Container = new Identity { Id = 321 },
                        ModelGump = 332
                    }),
                "0C000000030005020141010000034C01"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 14
                },
                new ServerEventData<AddItemToContainerEvent>(EventType.AddItemToContainer,
                    new AddItemToContainerEvent
                    {
                        Container = new Identity { Id = 321 },
                        Item = new Identity { Id = 123 }
                    }),
                "0E00000003000602017B0000000141010000"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 9
                },
                new ServerEventData<Identity>(EventType.AddMultipleItemsInCont, new Identity
                {
                    Id = 321
                }),
                "09000000030007010141010000"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 6
                },
                new ServerEventData<RejectMoveItemReason>(EventType.RejectMoveItem,
                    RejectMoveItemReason.OutOfSight),
                "06000000030008010602"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 9
                },
                new ServerEventData<Identity>(EventType.DrawObject, new Identity
                {
                    Id = 321
                }),
                "0900000003000A010141010000"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 14
                },
                new ServerEventData<MenuEvent>(EventType.Menu, new MenuEvent
                {
                    Dialog = new Identity { Id = 321 },
                    Menu = new Identity { Id = 321 }
                }),
                "0E00000003000B0201410100000141010000"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 15
                },
                new ServerEventData<MapMessageEvent>(EventType.MapMessage, new MapMessageEvent
                {
                    Item = new Identity { Id = 321 },
                    WorldPoint = new WorldPoint { X = 321, Y = 123 }
                }),
                "0F00000003000C030141010000034101037B00"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 11
                },
                new ServerEventData<AllowRefuseAttackEvent>(EventType.AllowRefuseAttack,
                    new AllowRefuseAttackEvent
                    {
                        Target = new Identity { Id = 321 },
                        IsAttackOk = false
                    }),
                "0B00000003000D0201410100000700"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 58
                },
                new ServerEventData<ClilocSpeechEvent>(EventType.ClilocSpeech,
                    new ClilocSpeechEvent
                    {
                        Character = new Character { Id = 111, Name = "Sample" },
                        Cliloc = new Identity { Id = 321 },
                        Text = "Sample text"
                    }),
                "3A00000003000E04016F000000000C000000530061006D0070006C00650001410100000016000000530061006D0070006C00650020007400650078007400"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 14
                },
                new ServerEventData<BuffDebuffSystemEvent>(EventType.BuffDebuffSystem,
                    new BuffDebuffSystemEvent
                    {
                        Object = new Identity { Id = 321 },
                        Attribute = 58,
                        IsEnabled = true
                    }),
                "0E000000030011030141010000033A000701"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 4
                },
                new ServerEventData<EmptyEvent>(EventType.ClientSendResync, new EmptyEvent()),
                "0400000003001200"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 12
                },
                new ServerEventData<CharAnimationEvent>(EventType.CharAnimation,
                    new CharAnimationEvent
                    {
                        Object = new Identity { Id = 321 },
                        Action = 1
                    }),
                "0C000000030013020141010000030100"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 24
                },
                new ServerEventData<IncomingGumpEvent>(EventType.IncomingGump,
                    new IncomingGumpEvent
                    {
                        Gump = new GumpIdentity { Serial = 111, Id = 8 },
                        Point = new ScreenPoint { X = 321, Y = -11 }
                    }),
                "1800000003001804016F0000000108000000024101000002F5FFFFFF"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 9
                },
                new ServerEventData<uint>(EventType.WindowsMessage, 1002),
                "0900000003001B0101EA030000"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 17
                },
                new ServerEventData<SoundEvent>(EventType.Sound, new SoundEvent
                {
                    Sound = new Identity { Id = 321 },
                    Point = new WorldPoint3D { X = 555, Y = 444, Z = -52 }
                }),
                "1100000003001C040141010000032B0203BC0105CC"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 6
                },
                new ServerEventData<bool>(EventType.Death, true),
                "0600000003001D010701"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 12
                },
                new ServerEventData<QuestArrowEvent>(EventType.QuestArrow,
                    new QuestArrowEvent { Point = new WorldPoint { X = 859, Y = 951 }, IsActive = false }),
                "0C00000003001E03035B0303B7030700"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 9
                },
                new ServerEventData<Identity>(EventType.PartyInvite, new Identity
                {
                    Id = 654
                }),
                "0900000003001F01018E020000"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 19
                },
                new ServerEventData<MapPinEvent>(EventType.MapPin, new MapPinEvent
                {
                    Identity = new Identity { Id = 123 },
                    Action = 8,
                    PinId = 89,
                    Point = new WorldPoint { X = 564, Y = 852 }
                }),
                "1300000003002005017B00000006080659033402035403"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 72
                },
                new ServerEventData<GumpTextEntryEvent>(EventType.GumpTextEntry,
                    new GumpTextEntryEvent
                    {
                        Identity = new Identity { Id = 321 },
                        Title = "Sample title",
                        InputStyle = 5,
                        MaxValue = 99,
                        Title2 = "Sample text"
                    }),
                "480000000300210501410100000018000000530061006D0070006C00650020007400690074006C006500060501630000000016000000530061006D0070006C00650020007400650078007400"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 37
                },
                new ServerEventData<GraphicalEffectEvent>(EventType.GraphicalEffect,
                    new GraphicalEffectEvent
                    {
                        Source = new Identity { Id = 5 },
                        SourcePoint = new WorldPoint3D { X = 859, Y = 985, Z = -5 },
                        Destination = new Identity { Id = 96 },
                        DestinationPoint = new WorldPoint3D { X = 3456, Y = 862, Z = -5 },
                        ItemId = 859,
                        Type = 8,
                        FixedDirection = Direction.NorthWest
                    }),
                "250000000300220B0105000000035B0303D90305FB016000000003800D035E0305FB0608035B030607"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 82
                },
                new ServerEventData<MessengerIncomingTextEvent>(EventType.MessengerEvent,
                    new MessengerIncomingTextEvent()
                    {
                        MessengerType = MessengerType.Telegram,
                        SenderNickname = "Test",
                        SenderId = "<ID>",
                        ChatId = "34FG7",
                        EventMsg = "Sample message",
                        EventType = MessengerEventType.Message
                    }),
                "520000000300240606010008000000540065007300740000080000003C00490044003E00000A00000033003400460047003700001C000000530061006D0070006C00650020006D006500730073006100670065000602"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 30
                },
                new ServerEventData<SetGlobalVarEvent>(
                    EventType.SetGlobalVar, 
                    new SetGlobalVarEvent { Name = "Var", Value = "Value" }),
                "1E000000030025020006000000560061007200000A000000560061006C0075006500"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 39
                },
                new ServerEventData<UpdateObjectStatsEvent>(EventType.UpdateObjStats,
                    new UpdateObjectStatsEvent
                    {
                        Object = new Identity { Id = 321 },
                        CurrentLife = 85,
                        MaxLife = 97,
                        CurrentMana = 84,
                        MaxMana = 84,
                        CurrentStamina = 9,
                        MaxStamina = 99
                    }),
                "27000000030026070141010000015500000001610000000154000000015400000001090000000163000000"
            },
            new object[]
            {
                new PacketHeader
                {
                    PacketType = PacketType.SCExecEventProc,
                    Length = 51
                },
                new ServerEventData<GlobalChatEvent>(EventType.GlobalChat, new GlobalChatEvent
                {
                    MsgCode = 8,
                    Name = "SAmple",
                    Text = "Sample text"
                }),
                "3300000003002703030800000C000000530041006D0070006C0065000016000000530061006D0070006C00650020007400650078007400"
            },
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}