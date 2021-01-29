#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthTypeMapper.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Drenalol.WaitingDictionary;
using StealthSharp.Enum;
using StealthSharp.Model;
using StealthSharp.Network;

namespace StealthSharp
{
    public class StealthTypeMapper : IStealthTypeMapper<ushort, ushort>
    {
        private readonly WaitingDictionary<ushort, Type?> _requestTypeMapper;
        public StealthTypeMapper()
        {
            _requestTypeMapper = new WaitingDictionary<ushort, Type?>();
        }

        public Task SetMappedTypeAsync(ushort request, Type? responseType)
        {
            return _requestTypeMapper.SetAsync(request, responseType);
        }

        public Task<Type?> GetMappedTypeAsync(ushort mappingId, ushort correlationId)
        {
            switch ((PacketType) mappingId)
            {
                case PacketType.SCZero:
                    return Task.FromResult<Type?>(null);
                case PacketType.SCReturnValue:
                    return _requestTypeMapper.WaitAsync(correlationId);
            }
            return Task.FromResult<Type?>(null);
        }

        //
        //
        //
        // switch ((PacketType) requestTypeIdentify)
        //             {
        //                 case PacketType.SCGetStealthInfo:
        //                     return typeof(AboutData);
        //                 case PacketType.SCGetGumpInfo:
        //                     return typeof(GumpInfo);
        //                 case PacketType.SCGetToolTipRec:
        //                     return typeof(List<ClilocItemRec>);
        //                 case PacketType.SCGetContextMenuRec:
        //                     return typeof(ContextMenu);
        //                 case PacketType.SCGetExtInfo:
        //                     return typeof(ExtendedInfo);
        //                 case PacketType.SCGetLandTilesArray:
        //                 case PacketType.SCGetStaticTilesArray:
        //                     return typeof(FoundTile);
        //                 case PacketType.SCUpdateFigure:
        //                     return typeof(IdMapFigure);
        //                 case PacketType.SCGetLandTileData:
        //                     return typeof(LandTileData);
        //                 case PacketType.SCGetCell:
        //                     return typeof(MapCell);
        //                 case PacketType.SCAddFigure:
        //                     return typeof(MapFigure);
        //                 case PacketType.SCGetMenuItemsEx:
        //                     return typeof(MenuItem);
        //                 case PacketType.SCGetMultis:
        //                     return typeof(MultiItem);
        //                 case PacketType.SCGetPathArray:
        //                     case PacketType.SCGetPathArray3D:
        //                     return typeof(MyPoint);
        //                 case PacketType.SCGetQuestArrow:
        //                     return typeof(Point);
        //                 case PacketType.SCReadStaticsXY:
        //                     return typeof(StaticItemRealXY);
        //                 case PacketType.SCGetStaticTileData:
        //                     return typeof(StaticTileData);
        //                 case PacketType.SCClientTargetResponse:
        //                     return typeof(TargetInfo);
        //                 
        //             }
        //             break;
        //         
        //     }
        //
        //     return null;
        // }
    }
}