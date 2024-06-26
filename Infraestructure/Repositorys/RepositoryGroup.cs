﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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


        //public async Task<IEnumerable<Tematicas>> getTematicByGroup(int id)
        //{
        //    IEnumerable<Tematicas> tematicas = null;
        //    try
        //    {
        //        using (MyContext ctx = new MyContext())
        //        {
        //            ctx.Configuration.LazyLoadingEnabled = false;

        //            var tematicasQuery = (
        //                from groupT in ctx.GroupT
        //                join assetsGroup in ctx.AssetsGroup on groupT.ID equals assetsGroup.GroupID
        //                join asset in ctx.Assets on assetsGroup.AssetsID equals asset.ID
        //                join tematica in ctx.Tematicas on asset.TematicaId equals tematica.TematicaID
        //                where groupT.ID == id
        //                select tematica
        //            );

        //            tematicas = await tematicasQuery.ToListAsync();

        //            // Cargar las temáticas padre por separado
        //            var tematicasPadreIds = tematicas
        //                .Where(t => t.ParentTematicaID.HasValue)
        //                .Select(t => t.ParentTematicaID.Value)
        //                .Distinct()
        //                .ToList();

        //            var tematicasPadre = await ctx.Tematicas
        //                .Where(t => tematicasPadreIds.Contains(t.TematicaID))
        //                .ToListAsync();

        //            // Asignar las temáticas padre a las respectivas temáticas
        //            foreach (var tematica in tematicas)
        //            {
        //                tematica.TematicaPadre = tematicasPadre.FirstOrDefault(t => t.TematicaID == tematica.ParentTematicaID);
        //            }
        //        }


        //        return tematicas;
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        string mensaje = "Error en la base de datos: \n" + dbEx.Message;
        //        throw new Exception(mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "Error en el servidor: \n" + ex.Message;
        //        throw;
        //    }
        //}

        public async Task<IEnumerable<OrderedTree>> getTematicByGroup(int id)
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

                    // Construir la estructura de árbol
                    var arbolTematicas = BuildTematicasTree(tematicas, tematicasPadre);

                    return OrderedList(arbolTematicas);
                }
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

        private List<TematicTree> BuildTematicasTree(IEnumerable<Tematicas> tematicas, List<Tematicas> tematicasPadre)
        {
            var dictionary = new List<Tematicas>();
            var result = new List<TematicTree>();

            foreach (var tematica in tematicas)
            {
                dictionary.Add(tematica);
            }

          
            using (MyContext ctx = new MyContext())
            {
                foreach (var item in dictionary)
                {
                    foreach (var tematicaPadre in tematicasPadre)

                    {
                        TematicTree tematicTree = new TematicTree();
                        if (item.ParentTematicaID == tematicaPadre.TematicaID)
                        {
                            Tematicas level1 = ctx.Tematicas.Where(a => a.TematicaID == tematicaPadre.ParentTematicaID).FirstOrDefault();
                            tematicTree.Id = level1 != null? level1.TematicaID : tematicaPadre.TematicaID;
                            tematicTree.Nombre = level1 != null ? level1.NombreTematica: tematicaPadre.NombreTematica;
                            tematicTree.Hijo = level1 != null ? tematicaPadre:item;
                            tematicTree.Nieto = level1 != null ? item:null;
                            result.Add(tematicTree);
                        }
                    }
                }
            }


                return result;
        }

        private List<OrderedTree> OrderedList(List<TematicTree> result)
        {
            try
            {
                if (result == null)
                {
                    return new List<OrderedTree>();
                }

                var groupedTemas = result
                    .GroupBy(t => new { t.Id, t.Nombre })
                    .Select(group => new OrderedTree
                    {
                        Id = group.Key.Id,
                        Nombre = group.Key.Nombre,
                        Hijos = group.Select(t => t.Hijo).Where(h => h != null).ToList(),
                        Nietos = group.Select(t => t.Nieto).Where(n => n != null).ToList()
                    })
                    .ToList();

                return groupedTemas;
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
        public async Task<Tematicas> GetCodColorByTematicaId(int tematicaId)
        {
            Tematicas oTematicas = new Tematicas();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("Sp_getColorByTematicaID", connection))
                        {
                            cmd.Parameters.Add("@TematicaId", SqlDbType.Int).Value = tematicaId;
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    oTematicas.TematicaID = Convert.ToInt32(reader["TematicaID"]);
                                    oTematicas.CodColor = reader["CodColor"].ToString();
                                   
                                }
                            }
                        }
                    }
                }
                return oTematicas;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error en la base de datos: " + dbEx.Message;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error al obtener el usuario: " + ex.Message;
                throw new Exception(mensaje);
            }
        }


    }
}
