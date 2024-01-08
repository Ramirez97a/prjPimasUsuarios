using ApplicationCore.Services;
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

        public async Task<ActionResult> Index(int userId)
        {
            IEnumerable<GroupT> lista = null;
            IServiceUsers _ServiceUsers = new ServiceUser();

            
            int id = userId;
            
            lista = await _ServiceUsers.getGropsByUser(id);
            //ViewBag.UserId = userId;
            TempData["UserId"] = id;

            return View(lista);
        }
        public ActionResult Grupos()
        {
            return View();
        }
    }
}