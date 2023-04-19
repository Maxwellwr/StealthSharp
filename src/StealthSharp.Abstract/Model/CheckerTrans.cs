#region Copyright

// -----------------------------------------------------------------------
// <copyright file="CheckerTrans.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Model
{
    [Serializable()]
    public class CheckerTransparency
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int Page;
        public int ElemNum;
    }
}