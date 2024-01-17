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

                Users Users = await service.Login(usermail, userPassword);

                if (Users == null)
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = "Usuario no autorizado ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Usuario autorizado";
                    response.userId = Users.ID;
                    Session["User"] = Users;
                }

                return Json(response);
            }
            catch (Exception e)
            {

                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return Json(response);
            }
        }

    }
}
