using System;
using System.Reflection;

namespace StealthSharp.Serialization
{
    public class TcpProperty
    {
        private readonly PropertyInfo _propertyInfo;

        public readonly TcpDataAttribute Attribute;
        public readonly bool IsValueType;
        public readonly TcpComposition Composition;
        public readonly Type PropertyType;
    }
}