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
    public class RepositoryAssets : IRepositoryAssets
    {
        public async Task<IEnumerable<Assets>> getAllSubtematic(int PtamaticId, int tematicId)
        {
            IEnumerable<Assets> assetsByTematicaWithParent = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    assetsByTematicaWithParent = await ctx.Assets
                       .Where(a => a.Tematicas.TematicaID == tematicId &&
                                   a.Tematicas.ParentTematicaID == PtamaticId)
                       .ToListAsync();

                    return assetsByTematicaWithParent;
                }

                return assetsByTematicaWithParent;
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

        public async Task<Assets> getAsset(int assetId)
        {
            Assets asset = new Assets();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    asset = await ctx.Assets
                        .Where(a => a.ID == assetId)
                        .FirstOrDefaultAsync();


                }

                return asset;
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

            public async Task<IEnumerable<Assets>> getByGroup(int id)
            {
                IEnumerable<Assets> assetsByTematica = null;
                try
                {
                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;

                        assetsByTematica = await ctx.GroupT
                            .Where(g => g.ID == id) // Filtrar por el ID del GroupT deseado
                            .SelectMany(g => g.AssetsGroup) // Obtener todos los AssetsGroup asociados al GroupT
                            .Select(ag => ag.Assets)
                            .Include(a => a.Tematicas)
                            .ToListAsync();


                    }

                    return assetsByTematica;
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

        public async Task<IEnumerable<Assets>> getByTematic(int tematicId)
        {
            IEnumerable<Assets> assetsByTematica = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    assetsByTematica = await ctx.Assets
                       .Where(a => a.Tematicas.TematicaID == tematicId)
                       .ToListAsync();


                }

                return assetsByTematica;
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
