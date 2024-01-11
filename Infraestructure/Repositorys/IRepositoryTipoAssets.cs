using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositorys
{
   public interface IRepositoryTipoAssets
    {
        Task<IEnumerable<TipoAssets>> getAll();
    }
}
