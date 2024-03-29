#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="DateTimeConverter.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;

#endregion

namespace StealthSharp.Serialization.Converters
{
    public class DateTimeConverter : ICustomConverter<DateTime>
    {
        private readonly IMarshaler _marshaler;

        public DateTimeConverter(IMarshaler marshaler)
        {
            _marshaler = marshaler;
        }

        public bool TryConvertToBytes(object? propertyValue, in Span<byte> span, Endianness endianness = Endianness.LittleEndian)
        {
            if (propertyValue is not DateTime dt) return false;

            try
            {
                _marshaler.Serialize(span, ToDouble(dt), endianness);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool TryConvertFromBytes(out object? propertyValue, in Span<byte> span, Endianness endianness = Endianness.LittleEndian)
        {
            try
            {
                _marshaler.Deserialize(span, typeof(double), out var value, endianness);
                propertyValue = ToDateTime((double)value);
            }
            catch
            {
                propertyValue = null;
                return false;
            }

            return true;
        }

        public int SizeOf(object? propertyValue)
        {
            return _marshaler.SizeOf(typeof(double));
        }

        /// <summary>
        ///     Converts a TDateTime from Delphi to a <see cref="System.DateTime" /> in .NET
        ///     For more info see:
        ///     http://docs.embarcadero.com/products/rad_studio/delphiAndcpp2009/HelpUpdate2/EN/html/delphivclwin32/System_TDateTime.html.
        /// </summary>
        /// <param name="tDateTime">Source double.</param>
        /// <returns>DateTime.</returns>
        private static DateTime ToDateTime(double tDateTime) => new DateTime(1899, 12, 30).AddDays(tDateTime);

        /// <summary>
        ///     Converts a <see cref="System.DateTime" /> from .NET to a TDateTime in Delphi.
        ///     For more info see:
        ///     http://docs.embarcadero.com/products/rad_studio/delphiAndcpp2009/HelpUpdate2/EN/html/delphivclwin32/System_TDateTime.html.
        /// </summary>
        /// <param name="dateTime">Source date-time.</param>
        /// <returns>Double represent of DateTime.</returns>
        private static double ToDouble(DateTime dateTime) => (dateTime - new DateTime(1899, 12, 30)).TotalDays;
    }
}