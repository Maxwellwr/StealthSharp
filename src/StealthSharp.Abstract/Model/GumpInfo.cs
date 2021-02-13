#region Copyright

// -----------------------------------------------------------------------
// <copyright file="GumpInfo.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    public class GumpInfo
    {
        public uint Serial { get; set; }

        public uint GumpID { get; set; }

        public ushort X { get; set; }

        public ushort Y { get; set; }

        public int Pages { get; set; }

        public bool NoMove { get; set; }

        public bool NoResize { get; set; }

        public bool NoDispose { get; set; }

        public bool NoClose { get; set; }

        
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