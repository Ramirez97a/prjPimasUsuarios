using Infraestructure.Models;
using Infraestructure.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceGroup : IServiceGroup
    {
        private IRepositoryGruop repository;
        public ServiceGroup()
        {
            repository = new RepositoryGroup();
        }

        public  async Task<GroupT> getbyId(int id)
        {
            return await repository.getbyId(id);
        }

        public async Task<IEnumerable<Tematicas>> getTematicByGroup(int id)
        {
            return await repository.getTematicByGroup(id);
        }
    }
}
