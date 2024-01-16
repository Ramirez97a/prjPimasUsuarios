using Infraestructure.Models;
using Infraestructure.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceAssets : IServiceAssets
    {
        private IRepositoryAssets repository;

        public ServiceAssets()
        {
            repository = new RepositoryAssets();
        }


        public async Task<IEnumerable<Assets>> getAllSubtematic(int PtamaticId, int tematicId)
        {
          return await  repository.getAllSubtematic(PtamaticId, tematicId);
        }

        public async Task<Assets> getAsset(int assetId)
        {
            return await repository.getAsset(assetId);
        }

        public async Task<IEnumerable<Assets>> getByGroup(int id)
        {
            return await repository.getByGroup(id);
        }

        public async Task<IEnumerable<Assets>> getByTematic(int tematicId, int group)
        {
            return await repository.getByTematic(tematicId,group);
        }

        public async Task<byte[]> getContend(int id)
        {
            return await repository.getContend(id);
        }   
      

        public FileShowContent AssetFileShow(int id)
        {
            return  repository.AssetFileShow(id);
        }

        public async Task<IEnumerable<Assets>> getByGroupLow(int group)
        {
            return await repository.getByGroupLow(group);
        }
    }
}
