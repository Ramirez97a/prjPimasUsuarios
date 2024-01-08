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
      
    }
}
