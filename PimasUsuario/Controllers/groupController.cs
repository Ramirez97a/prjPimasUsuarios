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
           
            ViewBag.UserId = userId;

            return View(lista);
        }
        public ActionResult Grupos()
        {
           

            return View();
        }
        public ActionResult ShowAssets(int id)
        {

            return View();
        }
        public FileResult archivo(int? id)
        {
            try
            {
                FileShowContent assets;
                IServiceAssets servicesAssets = new ServiceAssets();
                assets = servicesAssets.AssetFileShow(id.Value);

                if (assets != null)
                {

                }

                // Crear un objeto anónimo con los dos campos que deseas devolver
                return File(assets.Content, "application/pdf", assets.Title + ".pdf");

            }
            catch (Exception ex)            {
              
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return null;
            }
        }
    }
}