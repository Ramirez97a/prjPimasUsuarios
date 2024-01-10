using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositorys
{
    public interface IRepositoryAssets
    {
        Task<Assets> getAsset(int assetId);
        Task<IEnumerable<Assets>> getByTematic(int tematicId);
        Task<IEnumerable<Assets> >getAllSubtematic(int PtamaticId, int tematicId);
        Task<IEnumerable<Assets>> getByGroup(int id);
        Task<byte[]> getContend(int id);
    }
}
