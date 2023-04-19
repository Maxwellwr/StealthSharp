#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="EventDataTypeAttribute.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;

#endregion

namespace StealthSharp.Serialization
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EventDataTypeAttribute : Attribute
    {
        public Type DataType { get; }

        public EventDataTypeAttribute(Type dataType)
        {
            DataType = dataType;
        }
    }
}