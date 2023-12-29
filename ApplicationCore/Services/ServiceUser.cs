using Infraestructure.Models;
using Infraestructure.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUser : IServiceUsers
    {
       private IRepositoryUsers repository;
        public ServiceUser()
        {
            repository = new RepositoryUsers();
        }
        public async Task<IEnumerable<Users>> getAll()
        {
            return await repository.getAll();
        }

        public async Task<Users> getById(int id)
        {
            return await repository.getById(id);
        }

        public async Task<IEnumerable<GroupT>> getGropsByUser(int userId)
        {
            return await repository.getGropsByUser(userId);
        }

        public async Task<Users> Login(string usermail, string userPassword)
        {
            string codifiedPass = Security.EncrypthAES(userPassword);
            return await repository.Login(usermail, codifiedPass);
        }
    }
}
