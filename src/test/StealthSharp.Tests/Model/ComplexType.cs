#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="ComplexType.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using StealthSharp.Serialization;

#endregion

namespace StealthSharp.Tests.Model
{
    [Serializable]
    public class ComplexType
    {
        public int Property1 { get; set; }
        public short Property2 { get; set; }
        public string? Property3 { get; set; }

        public ComplexType? InnerComplexType { get; set; }
    }
}