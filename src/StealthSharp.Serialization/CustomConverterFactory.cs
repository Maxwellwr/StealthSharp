#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="CustomConverterFactory.cs" company="StealthSharp">
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
    public class CustomConverterFactory : ICustomConverterFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomConverterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool TryGetConverter(Type propertyType, out ICustomConverter? customConverter)
        {
            var genericServiceType = typeof(ICustomConverter<>).MakeGenericType(propertyType);
            customConverter = _serviceProvider.GetService(genericServiceType) as ICustomConverter;
            return customConverter is not null || (propertyType.BaseType is not null && TryGetConverter(propertyType.BaseType, out customConverter));
        }
    }
}