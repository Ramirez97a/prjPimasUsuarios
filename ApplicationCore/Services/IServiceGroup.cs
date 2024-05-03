using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceGroup
    {
        Task<IEnumerable<OrderedTree>> getTematicByGroup(int id);
        Task<GroupT> getbyId(int assetId);
        Task<Tematicas> GetCodColorByTematicaId(int tematicaId);

    }
}
