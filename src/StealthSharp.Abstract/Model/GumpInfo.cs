#region Copyright

// -----------------------------------------------------------------------
// <copyright file="GumpInfo.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using StealthSharp.Serialization;

namespace StealthSharp.Model
{
    public class GumpInfo
    {
        [PacketData(0, 4)] public uint Serial { get; set; }

        [PacketData(4, 4)] public uint GumpID { get; set; }

        [PacketData(8, 2)] public ushort X { get; set; }

        [PacketData(10, 2)] public ushort Y { get; set; }

        [PacketData(12, 4)] public int Pages { get; set; }

        [PacketData(16, 1)] public bool NoMove { get; set; }

        [PacketData(17, 1)] public bool NoResize { get; set; }

        [PacketData(18, 1)] public bool NoDispose { get; set; }

        [PacketData(19, 1)] public bool NoClose { get; set; }

        [PacketData(20, PacketDataType = PacketDataType.Dynamic)]
        public ExtGumpInfo ExtData { get; set; }
    }

    public class ExtGumpInfo
    {
        public Group[] Groups { get; set; }

        public EndGroup[] EndGroups { get; set; }

        public GumpButton[] GumpButtons { get; set; }

        public ButtonTileArt[] ButtonTileArts { get; set; }

        public CheckBox[] CheckBoxes { get; set; }

        public CheckerTransparency[] CheckerTrans { get; set; }

        public CroppedText[] CroppedText { get; set; }

        public GumpPic[] GumpPics { get; set; }

        public GumpPicTiled[] GumpPicTiled { get; set; }

        public RadioButton[] RadioButtons { get; set; }

        public ResizePic[] ResizePics { get; set; }

        public GumpText[] GumpText { get; set; }

        public TextEntry[] TextEntries { get; set; }

        public string[] Text { get; set; }

        public TextEntryLimited[] TextEntriesLimited { get; set; }

        public TilePic[] TilePics { get; set; }

        public TilePicture[] TilePicHue { get; set; }

        public Tooltip[] Tooltips { get; set; }

        public HtmlGump[] HtmlGump { get; set; }

        public XmfHTMLGump[] XmfHtmlGump { get; set; }

        public XmfHTMLGumpColor[] XmfHTMLGumpColor { get; set; }

        public XmfHTMLTok[] XmfHTMLTok { get; set; }

        public ItemProperty[] ItemProperties { get; set; }
    }
}