#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IGlobalService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Threading.Tasks;
using StealthSharp.Enumeration;

#endregion

namespace StealthSharp.Services
{
    public interface IGlobalService
    {
        Task<string> GetGlobalAsync(VarRegion globalRegion, string varName);

        Task SetGlobalAsync(VarRegion globalRegion, string varName, string varValue);
    }
}