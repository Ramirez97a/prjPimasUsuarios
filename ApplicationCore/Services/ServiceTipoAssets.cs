using Infraestructure.Models;
using Infraestructure.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoAssets : IServiceTipoAssets
    {
        private IRepositoryTipoAssets repository;
        public ServiceTipoAssets()
        {
            repository = new RepositoryTipoAssets();
        }
        public async Task<IEnumerable<TipoAssets>> getAll()
        {
            return await repository.getAll();  
        }
    }
}
