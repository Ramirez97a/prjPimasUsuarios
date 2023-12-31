﻿using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PimasUsuario.Controllers
{
    public class groupController : Controller
    {
        // GET: group

        public async Task<ActionResult> Index()
        {
            IEnumerable<GroupT> lista = null;
            IServiceUsers _ServiceUsers = new ServiceUser();

            //Users userid = (Users)Session["userid"];
            //int id = userid.ID;
            // Asegúrate de que el método getGropsByUser es async
            lista = await _ServiceUsers.getGropsByUser(1);

            return View(lista);
        }
        public ActionResult Grupos()
        {
            return View();
        }
    }
}