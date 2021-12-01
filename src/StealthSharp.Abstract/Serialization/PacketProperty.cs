#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PacketProperty.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Reflection;

namespace StealthSharp.Serialization
{
    public class PacketProperty
    {
        private readonly PropertyInfo _propertyInfo;
        public Type PropertyType { get; }

        public PacketProperty(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
            PropertyType = propertyInfo.PropertyType;
        }

        public object? Get(object data) =>
            _propertyInfo.GetValue(data);

        public void Set(object input, object? value) =>
            _propertyInfo.SetValue(input, value);
    }
}