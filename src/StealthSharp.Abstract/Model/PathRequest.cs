#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="PathRequest.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

namespace StealthSharp.Model
{
    public class PathReqeust
    {
        public ushort StartX { get; set; }
        public ushort StartY { get; set; }
        public sbyte StartZ { get; set; }
        public ushort FinishX { get; set; }
        public ushort FinishY { get; set; }
        public sbyte FinishZ { get; set; }
        public byte WorldNum { get; set; }
        public int AccuracyXy { get; set; }
        public int AccuracyZ { get; set; }
        public bool Run { get; set; }
    }
}