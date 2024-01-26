using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositorys
{
   public interface IRepositoryGruop
    {
        Task<IEnumerable<TematicTree>> getTematicByGroup(int id);
        Task<GroupT> getbyId(int assetId);

    }
}
