#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="ActivatorHelper.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

#endregion

namespace StealthSharp.Serialization
{
    public static class ActivatorHelper
    {
        // Constructor lookup and Invoke dominate the cost of creating an object per event or model, so both are cached.
        private static readonly ConcurrentDictionary<Type, Func<object>> ParameterlessFactories = new();
        private static readonly ConcurrentDictionary<ConstructorKey, ConstructorInfo?> Constructors = new();

        public static object CreateInstanceParameterless(Type targetType)
        {
            //string test first - it has no parameterless constructor
            if (Type.GetTypeCode(targetType) == TypeCode.String)
                return string.Empty;

            var targetObject = ParameterlessFactories.GetOrAdd(targetType, CreateParameterlessFactory)();
            if (targetObject == null)
                throw new ArgumentException("Unable to instantiate type: " + targetType.AssemblyQualifiedName + " - Unknown Error");
            return targetObject;
        }

        private static Func<object> CreateParameterlessFactory(Type targetType)
        {
            // get the default constructor and instantiate
            var info = targetType.GetConstructor(Type.EmptyTypes);
            if (info != null)
                return Expression.Lambda<Func<object>>(Expression.Convert(Expression.New(info), typeof(object))).Compile();

            //must not have found the constructor
            if (targetType.IsValueType || (targetType.BaseType is not null && targetType.BaseType.IsEnum))
                return () => Activator.CreateInstance(targetType)!;

            return () => throw new ArgumentException("Unable to instantiate type: " + targetType.AssemblyQualifiedName + " - Constructor not found");
        }

        public static object CreateInstance(Type targetType, params object[] parameters)
        {
            // get the constructor with exactly these parameter types and instantiate
            var types = new Type[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
                types[i] = parameters[i].GetType();

            var info = Constructors.GetOrAdd(new ConstructorKey(targetType, types),
                key => key.TargetType.GetConstructor(key.ParameterTypes));
            if (info == null) //must not have found the constructor
                throw new ArgumentException("Unable to instantiate type: " + targetType.AssemblyQualifiedName + " - Constructor not found");

            var targetObject = info.Invoke(parameters);
            if (targetObject == null)
                throw new ArgumentException("Unable to instantiate type: " + targetType.AssemblyQualifiedName + " - Unknown Error");
            return targetObject;
        }

        private readonly struct ConstructorKey : IEquatable<ConstructorKey>
        {
            public ConstructorKey(Type targetType, Type[] parameterTypes)
            {
                TargetType = targetType;
                ParameterTypes = parameterTypes;
            }

            public Type TargetType { get; }
            public Type[] ParameterTypes { get; }

            public bool Equals(ConstructorKey other)
            {
                return TargetType == other.TargetType && ParameterTypes.AsSpan().SequenceEqual(other.ParameterTypes);
            }

            public override bool Equals(object? obj)
            {
                return obj is ConstructorKey other && Equals(other);
            }

            public override int GetHashCode()
            {
                var hash = new HashCode();
                hash.Add(TargetType);
                foreach (var type in ParameterTypes)
                    hash.Add(type);
                return hash.ToHashCode();
            }
        }
    }
}