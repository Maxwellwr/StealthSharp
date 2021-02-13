#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="DateTimeConverter.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;

namespace StealthSharp.Serialization
{
    public class DateTimeConverter:ICustomConverter<DateTime>
    {
        private readonly IPacketSerializer _bitConvert;
        private readonly IMarshaler _marshaler;
        public DateTimeConverter(IPacketSerializer bitConvert, IMarshaler marshaler)
        {
            _bitConvert = bitConvert;
            _marshaler = marshaler;
        }
        
        public bool TryConvertToBytes(object? propertyValue, in Span<byte> span, Endianness endianness = Endianness.LittleEndian)
        {
            if (propertyValue is not DateTime dt)
            {
                return false;
            }
            else
            {
                try
                {
                    _bitConvert.Serialize(span,ToDouble(dt), endianness);
                }
                catch
                {
                    return false;
                }
                
                return true;
            }
        }

        public bool TryConvertFromBytes(out object? propertyValue, in Span<byte> span, Endianness endianness = Endianness.LittleEndian)
        {
            try
            {
                _bitConvert.Deserialize(span, typeof(double), out var value,endianness);
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
        private DateTime ToDateTime(double tDateTime)
        {
            var startDate = new DateTime(1899, 12, 30);
            var days = (int) tDateTime;
            var hours = 24 * (tDateTime - days);
            return startDate.AddDays(days).AddHours(hours);
        }

        /// <summary>
        ///     Converts a <see cref="System.DateTime" /> from .NET to a TDateTime in Delphi.
        ///     For more info see:
        ///     http://docs.embarcadero.com/products/rad_studio/delphiAndcpp2009/HelpUpdate2/EN/html/delphivclwin32/System_TDateTime.html.
        /// </summary>
        /// <param name="dateTime">Source date-time.</param>
        /// <returns>Double represent of DateTime.</returns>
        private double ToDouble(DateTime dateTime)
        {
            var startDate = new DateTime(1899, 12, 30);
            var deltaDate = dateTime - startDate;

            var days = deltaDate.Days;
            deltaDate -= new TimeSpan(days, 0, 0, 0);

            var hours = deltaDate.TotalSeconds / 3600.0 / 24;

            return days + hours;
        }
    }
}