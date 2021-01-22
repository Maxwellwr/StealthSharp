using System;
using System.Reflection;

namespace StealthSharp.Serialization
{
    public class PacketProperty
    {
        private readonly PropertyInfo _propertyInfo;

        public readonly PacketDataAttribute Attribute;
        public readonly Type PropertyType;

        public PacketProperty(PropertyInfo propertyInfo, PacketDataAttribute attribute)
        {
            Attribute = attribute;
            PropertyType = propertyInfo.PropertyType;
            _propertyInfo = propertyInfo;
        }
        
        public object? Get(object data) => 
            _propertyInfo.GetValue(data);
        
        public void Set(object input, object? value)=>
            _propertyInfo.SetValue(input, value);
    }
}