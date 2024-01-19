using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositorys
{
    public class RepositoryGroup : IRepositoryGruop
    {

        public async Task<GroupT> getbyId(int id)
        {
            GroupT group = new GroupT();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    group = await ctx.GroupT
                        .Where(a => a.ID == id)
                        .FirstOrDefaultAsync();


                }

                return group;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error en la base de datos: \n" + dbEx.Message;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error en el servidor: \n" + ex.Message;
                throw;
            }
        }


        public async Task<IEnumerable<Tematicas>> getTematicByGroup(int id)
        {
            IEnumerable<Tematicas> tematicas = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    tematicas = await (from groupT in ctx.GroupT
                                       join assetsGroup in ctx.AssetsGroup on groupT.ID equals assetsGroup.GroupID
                                       join asset in ctx.Assets on assetsGroup.AssetsID equals asset.ID
                                       join tematica in ctx.Tematicas on asset.TematicaId equals tematica.TematicaID
                                       join parentTematica in ctx.Tematicas on tematica.ParentTematicaID equals parentTematica.TematicaID into parentGroup
                                       from parent in parentGroup.DefaultIfEmpty()
                                       where groupT.ID == id
                                       select new Tematicas
                                       {
                                           TematicaID = tematica.TematicaID,
                                           // Otras propiedades de Tematicas que necesites

                                           // Propiedad adicional para almacenar el nombre de la temática padre
                                           ParentTematicaNombre = parent != null ? parent.NombreTematica : null
                                       }).ToListAsync();
                }

                return tematicas;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error en la base de datos: \n" + dbEx.Message;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error en el servidor: \n" + ex.Message;
                throw;
            }
        }
    }
}
