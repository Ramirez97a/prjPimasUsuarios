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

                    User = ctx.Users.Where(p => p.Email == usermail && p.Password.Equals(userPassword)).FirstOrDefault<Users>();
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


    }
}
