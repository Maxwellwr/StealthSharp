#region Copyright

// -----------------------------------------------------------------------
// <copyright file="GumpInfo.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Model
{
    [Serialization.Serializable()]
    public class GumpInfo
    {
        public uint Serial { get; set; }

        public uint GumpId { get; set; }

        public ushort X { get; set; }

        public ushort Y { get; set; }

        public int Pages { get; set; }

        public bool NoMove { get; set; }

        public bool NoResize { get; set; }

        public bool NoDispose { get; set; }

        public bool NoClose { get; set; }


        public ExtGumpInfo ExtData { get; set; } = new();
    }

    [Serialization.Serializable()]
    public class ExtGumpInfo
    {
        public Group[] Groups { get; set; } = Array.Empty<Group>();

        public EndGroup[] EndGroups { get; set; }= Array.Empty<EndGroup>();

        public GumpButton[] GumpButtons { get; set; }= Array.Empty<GumpButton>();

        public ButtonTileArt[] ButtonTileArts { get; set; }= Array.Empty<ButtonTileArt>();

        public CheckBox[] CheckBoxes { get; set; }= Array.Empty<CheckBox>();

        public CheckerTransparency[] CheckerTrans { get; set; }= Array.Empty<CheckerTransparency>();

        public CroppedText[] CroppedText { get; set; }= Array.Empty<CroppedText>();

        public GumpPic[] GumpPics { get; set; }= Array.Empty<GumpPic>();

        public GumpPicTiled[] GumpPicTiled { get; set; }= Array.Empty<GumpPicTiled>();

        public RadioButton[] RadioButtons { get; set; }= Array.Empty<RadioButton>();

        public ResizePic[] ResizePics { get; set; }= Array.Empty<ResizePic>();

        public GumpText[] GumpText { get; set; }= Array.Empty<GumpText>();

        public TextEntry[] TextEntries { get; set; }= Array.Empty<TextEntry>();

        public string[] Text { get; set; }= Array.Empty<string>();

        public TextEntryLimited[] TextEntriesLimited { get; set; }= Array.Empty<TextEntryLimited>();

        public TilePic[] TilePics { get; set; }= Array.Empty<TilePic>();

        public TilePicture[] TilePicHue { get; set; }= Array.Empty<TilePicture>();

        public Tooltip[] Tooltips { get; set; }= Array.Empty<Tooltip>();

        public HtmlGump[] HtmlGump { get; set; }= Array.Empty<HtmlGump>();

        public XmfHTMLGump[] XmfHtmlGump { get; set; }= Array.Empty<XmfHTMLGump>();

        public XmfHTMLGumpColor[] XmfHTMLGumpColor { get; set; }= Array.Empty<XmfHTMLGumpColor>();

        public XmfHTMLTok[] XmfHTMLTok { get; set; }= Array.Empty<XmfHTMLTok>();

        public ItemProperty[] ItemProperties { get; set; }= Array.Empty<ItemProperty>();
    }
}