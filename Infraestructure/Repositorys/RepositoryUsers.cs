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
    public class RepositoryUsers : IRepositoryUsers
    {
        public async Task<IEnumerable<Users>> getAll()
        {
            IEnumerable<Users> list = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = await ctx.Users.AsNoTracking().ToListAsync();

                }

                return list;
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

        public async Task<Users> getById(int id)
        {
            Users users = new Users();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    users = await ctx.Users
                                .Include(u => u.Comments)
                                .Include(u => u.UsersGroupT)
                                .Include(u => u.Wall)
                                .Include(u => u.Organization)
                                .Include(u => u.Status).Where(u=> u.ID==id).FirstOrDefaultAsync();


                }

                return users;
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

        public async Task<IEnumerable<GroupT>> getGropsByUser(int userId)
        {
            IEnumerable<GroupT> groups = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    // Consulta LINQ para obtener los GroupT asociados al usuario con el ID proporcionado.
                    groups = await ctx.Users
                        .Where(u => u.ID == userId)
                        .SelectMany(u => u.UsersGroupT)
                        .Select(ug => ug.GroupT)
                        .ToListAsync();
                }

                return groups;
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

        public async Task<Users> Login(string usermail, string userPassword)
        {
            try
            {
                Users User = new Users();
                using (MyContext ctx = new MyContext())
                {
                    User = await ctx.Users.Include("UsersGroupT")
                                  .Where(p => p.Email == usermail && p.Password.Equals(userPassword))
                                  .FirstOrDefaultAsync();
                }

                return User;
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

        public async Task<Users> Register(Users usuario)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    using (var connection = new SqlConnection(ctx.Database.Connection.ConnectionString))
                    {
                        await connection.OpenAsync();

                        using (var command = new SqlCommand("SPRegisterUser", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Parámetros del procedimiento almacenado
                            command.Parameters.AddWithValue("@ID", usuario.ID);
                            command.Parameters.AddWithValue("@StatusID", usuario.StatusID);
                            command.Parameters.AddWithValue("@Email", usuario.Email);
                            command.Parameters.AddWithValue("@Name", usuario.Name);
                            command.Parameters.AddWithValue("@Surname", usuario.Surname);
                            command.Parameters.AddWithValue("@Password", usuario.Password);
                            command.Parameters.AddWithValue("@Profile", usuario.Profile);
                            command.Parameters.AddWithValue("@ExpirationDate", usuario.ExpirationDate ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@EmailActive", usuario.EmailActive);
                            command.Parameters.AddWithValue("@InvitationCant", usuario.InvitationCant);

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                }

                return usuario;  // Devuelve el objeto Users después de la inserción
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
