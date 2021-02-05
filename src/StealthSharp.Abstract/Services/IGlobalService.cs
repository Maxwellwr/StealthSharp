#region Copyright

// -----------------------------------------------------------------------
// <copyright file="IGlobalService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.Threading.Tasks;
using StealthSharp.Enum;

namespace StealthSharp.Services
{
    public interface IGlobalService
    {
        Task<string> GetGlobalAsync(VarRegion globalRegion, string varName);

        void SetGlobal(VarRegion globalRegion, string varName, string varValue);
    }
}