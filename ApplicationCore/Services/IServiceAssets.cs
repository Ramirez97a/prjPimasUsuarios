﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceAssets
    {
        Task<Assets> getAsset(int assetId);
        Task<IEnumerable<Assets>> getByTematic(int tematicId, int group);
        Task<IEnumerable<Assets>> getAllSubtematic(int PtamaticId, int tematicId);
        Task<IEnumerable<Assets>> getByGroup(int id);
        Task<byte[]> getContend(int id);
        Task<IEnumerable<Assets>> getByGroupLow(int group);
        FileShowContent AssetFileShow(int id);
    }
}
