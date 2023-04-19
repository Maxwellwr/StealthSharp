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
using System.Linq;

#endregion

namespace StealthSharp.Serialization
{
    public static class ActivatorHelper
    {
        public static object CreateInstanceParameterless(Type targetType)
        {
            //string test first - it has no parameterless constructor
            if (Type.GetTypeCode(targetType) == TypeCode.String)
                return string.Empty;

            // get the default constructor and instantiate
            Type[] types = Type.EmptyTypes;
            var info = targetType.GetConstructor(types);
            object? targetObject = null;

            if (info == null) //must not have found the constructor
                if (targetType.IsValueType || (targetType.BaseType is not null && targetType.BaseType.IsEnum))
                    targetObject = Activator.CreateInstance(targetType);
                else
                    throw new ArgumentException("Unable to instantiate type: " + targetType.AssemblyQualifiedName + " - Constructor not found");
            else
                targetObject = info.Invoke(null);

            if (targetObject == null)
                throw new ArgumentException("Unable to instantiate type: " + targetType.AssemblyQualifiedName + " - Unknown Error");
            return targetObject;
        }

        public static object CreateInstance(Type targetType, params object[] parameters)
        {
            // get the default constructor and instantiate
            Type[] types = parameters.Select(p => p.GetType()).ToArray();
            var info = targetType.GetConstructor(types);
            object? targetObject = null;

            if (info == null) //must not have found the constructor
                throw new ArgumentException("Unable to instantiate type: " + targetType.AssemblyQualifiedName + " - Constructor not found");
            targetObject = info.Invoke(parameters);

            if (targetObject == null)
                throw new ArgumentException("Unable to instantiate type: " + targetType.AssemblyQualifiedName + " - Unknown Error");
            return targetObject;
        }
    }
}