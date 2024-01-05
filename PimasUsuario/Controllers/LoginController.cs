using ApplicationCore.Services;
using Infraestructure.Models;
using PimasUsuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PimasUsuario.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult ForgotPasswordChange()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> LoginUser(string usermail, string userPassword)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceUsers service = new ServiceUser();

                Users users = await service.Login(usermail, userPassword);

                if (users == null)
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = "Usuario no autorizado ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Usuario autorizado";
                    response.Data = users;
                    return Json(new { status = "OK", message = "Login successful", data = response }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { status = "OK", message = "Login successful", data = response }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "Error interno del servidor. Detalles: " + e.Message;
                // Loguea la excepción para obtener más detalles en los registros
               
                return Json(response);
            }
        }




    }
}
