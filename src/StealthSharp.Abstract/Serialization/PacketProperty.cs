#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PacketProperty.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Linq.Expressions;
using System.Reflection;

#endregion

namespace StealthSharp.Serialization
{
    public class PacketProperty
    {
        private readonly PropertyInfo _propertyInfo;
        // Compiled lazily: reflection GetValue/SetValue is far too slow for the marshaler hot path.
        private Func<object, object?>? _getter;
        private Action<object, object?>? _setter;
        private readonly bool _useReflection;

        public Type PropertyType { get; }

        public PacketProperty(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
            PropertyType = propertyInfo.PropertyType;
            // Setting a property of a boxed struct through a compiled delegate would change a copy.
            _useReflection = propertyInfo.DeclaringType?.IsValueType ?? true;
        }

        public object? Get(object data)
        {
            if (_useReflection)
                return _propertyInfo.GetValue(data);

            return (_getter ??= CompileGetter())(data);
        }

        public void Set(object input, object? value)
        {
            if (_useReflection || _propertyInfo.SetMethod is null)
            {
                _propertyInfo.SetValue(input, value);
                return;
            }

            (_setter ??= CompileSetter())(input, value);
        }

        private Func<object, object?> CompileGetter()
        {
            var instance = Expression.Parameter(typeof(object));
            var body = Expression.Convert(
                Expression.Property(Expression.Convert(instance, _propertyInfo.DeclaringType!), _propertyInfo),
                typeof(object));
            return Expression.Lambda<Func<object, object?>>(body, instance).Compile();
        }

        private Action<object, object?> CompileSetter()
        {
            var instance = Expression.Parameter(typeof(object));
            var value = Expression.Parameter(typeof(object));
            var body = Expression.Call(
                Expression.Convert(instance, _propertyInfo.DeclaringType!),
                _propertyInfo.SetMethod!,
                Expression.Convert(value, PropertyType));
            return Expression.Lambda<Action<object, object?>>(body, instance, value).Compile();
        }
    }
}