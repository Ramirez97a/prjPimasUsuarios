﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsers
    {
        Task<IEnumerable<Users>> getAll();
        Task<Users> Login(string usermail, string userPassword);
        Task<IEnumerable<GroupT>> getGropsByUser(int userId);
        Task<Users> getById(int id);
        Task<Users> Register(Users user);
    }
}
