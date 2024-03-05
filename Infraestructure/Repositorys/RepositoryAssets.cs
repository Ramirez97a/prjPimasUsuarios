using Infraestructure.Models;
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

        public async Task<IEnumerable<Assets>> getByTematic(int tematicId, int group)
        {
            IEnumerable<Assets> assetsByTematica = null;
            try
            {
                List<Assets> listaPartial = new List<Assets>();
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    using (var connection = new SqlConnection(ctx.Database.Connection.ConnectionString))
                    {
                        await connection.OpenAsync();

                        using (var command = new SqlCommand("GetAssetsByGroupAndTheme", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Parámetros del procedimiento almacenado
                            command.Parameters.AddWithValue("@GroupID", group);
                            command.Parameters.AddWithValue("@TematicaID", tematicId);

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (reader.Read())
                                {
                                    Assets assets = new Assets
                                    {
                                        ID = Convert.ToInt32(reader["ID"].ToString()),
                                        Title = reader["Title"].ToString(),
                                        Description = reader["Description"].ToString(),
                                        Image = reader["Image"] != DBNull.Value ? (byte[])reader["Image"] : null,
                                        TypeContent = reader["TypeContent"].ToString(),
                                        ExternalLink = reader["ExternalLink"] != DBNull.Value ? reader["ExternalLink"].ToString() : "",
                                        AssetPublic = reader["AssetPublic"].ToString(),
                                        Downloadable = (bool)reader["Downloadable"],
                                        TipoAssetID = reader["TipoAssetID"] != DBNull.Value ? Convert.ToInt32(reader["TipoAssetID"]) : 0,
                                        TematicaId = Convert.ToInt32(reader["TematicaId"].ToString()),
                                    };

                                    listaPartial.Add(assets);
                                }
                            }
                        }
                    }
                }
                return listaPartial;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error en la base de datos: \n{dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servidor: \n{ex.Message}");
            }
        }


        public async Task<byte[]> getContend(int id)
        {
            byte[] assetsByTematica;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    assetsByTematica = await ctx.Assets
                        .Where(asset => asset.ID == id)
                .Select(asset => asset.Content)
                .FirstOrDefaultAsync();

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
        public FileShowContent AssetFileShow(int id)
        {
            FileShowContent assets = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    using (var connection = new SqlConnection(ctx.Database.Connection.ConnectionString))
                    {
                        connection.Open();

                        using (var command = new SqlCommand("USP_GetAssetsByAssestID", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@AssestID", id));

                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    assets = new FileShowContent
                                    {
                                        Title = reader["Title"].ToString(),
                                        Content = (byte[])reader["Content"]
                                    };
                                }
                            }
                        }
                    }
                }
                return assets;
            }
            catch (DbUpdateException dbEx)
            {
              
                throw dbEx;
            }
            catch (Exception ex)
            {
              
                throw ex;
            }
        }

        public async Task<IEnumerable<Assets>> getByGroupLow(int group)
        {
            IEnumerable<Assets> assetsByTematica = null;
            try
            {
                List<Assets> listaPartial = new List<Assets>();
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    using (var connection = new SqlConnection(ctx.Database.Connection.ConnectionString))
                    {
                        await connection.OpenAsync();

                        using (var command = new SqlCommand("GetAssetsByGroupLow", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Parámetros del procedimiento almacenado
                            command.Parameters.AddWithValue("@GroupID", group);

                            using (var reader = command.ExecuteReader())
                            {
                                while (await reader.ReadAsync())
                                {
                                    Assets assets = new Assets
                                    {
                                        ID = Convert.ToInt32(reader["ID"].ToString()),
                                        Title = reader["Title"].ToString(),
                                        Description = reader["Description"].ToString(),
                                        Image = reader["Image"] != DBNull.Value ? (byte[])reader["Image"] : null,
                                        TypeContent = reader["TypeContent"].ToString(),
                                        ExternalLink = reader["ExternalLink"] != DBNull.Value ? reader["ExternalLink"].ToString() : "",
                                        AssetPublic = reader["AssetPublic"].ToString(),
                                        Downloadable = reader["Downloadable"] != DBNull.Value ? (bool)reader["Downloadable"]: false ,
                                        TipoAssetID = reader["TipoAssetID"] != DBNull.Value ? Convert.ToInt32(reader["TipoAssetID"]) : 0,
                                        TematicaId = Convert.ToInt32(reader["TematicaId"].ToString()),
                                    };

                                    listaPartial.Add(assets);
                                }
                            }
                        }
                    }
                }
                return listaPartial;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error en la base de datos: \n{dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servidor: \n{ex.Message}");
            }
        }
    }
}