﻿using Infraestructure.Models;
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

                    var tematicasQuery = (
                        from groupT in ctx.GroupT
                        join assetsGroup in ctx.AssetsGroup on groupT.ID equals assetsGroup.GroupID
                        join asset in ctx.Assets on assetsGroup.AssetsID equals asset.ID
                        join tematica in ctx.Tematicas on asset.TematicaId equals tematica.TematicaID
                        where groupT.ID == id
                        select tematica
                    );

                    tematicas = await tematicasQuery.ToListAsync();

                    // Cargar las temáticas padre por separado
                    var tematicasPadreIds = tematicas
                        .Where(t => t.ParentTematicaID.HasValue)
                        .Select(t => t.ParentTematicaID.Value)
                        .Distinct()
                        .ToList();

                    var tematicasPadre = await ctx.Tematicas
                        .Where(t => tematicasPadreIds.Contains(t.TematicaID))
                        .ToListAsync();

                    // Asignar las temáticas padre a las respectivas temáticas
                    foreach (var tematica in tematicas)
                    {
                        tematica.TematicaPadre = tematicasPadre.FirstOrDefault(t => t.TematicaID == tematica.ParentTematicaID);
                    }
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
