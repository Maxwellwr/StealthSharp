#region Copyright

// -----------------------------------------------------------------------
// <copyright file="AboutData.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;

#endregion

namespace StealthSharp.Model
{
    /// <summary>
    ///     About data.
    /// </summary>
    [Serialization.Serializable()]
    public class AboutData
    {
        public ushort[] StealthVersion { get; set; } = new ushort[0];
        public ushort Build { get; init; }
        public DateTime BuildDate { get; init; }
        public ushort GitRevNumber { get; init; }
        public string? GitRevision { get; set; }

        protected bool Equals(AboutData other)
        {
            return Build == other.Build && BuildDate.Equals(other.BuildDate) && GitRevNumber == other.GitRevNumber;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((AboutData)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Build, BuildDate, GitRevNumber);
        }

        public static bool operator ==(AboutData left, AboutData right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AboutData left, AboutData right)
        {
            return !Equals(left, right);
        }
    }
}