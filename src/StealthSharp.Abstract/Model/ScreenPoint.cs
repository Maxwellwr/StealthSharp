﻿#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Point.cs" company="StealthSharp">
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
    ///     Point.
    /// </summary>
    [Serialization.Serializable()]
    public class ScreenPoint
    {
        public int X { get; init; }
        public int Y { get; init; }

        protected bool Equals(ScreenPoint other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ScreenPoint)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}