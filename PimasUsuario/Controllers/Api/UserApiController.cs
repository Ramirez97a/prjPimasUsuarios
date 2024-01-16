using ApplicationCore.Services;
using Infraestructure.Models;
using PimasUsuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PimasUsuario.Controllers.Api
{
    [RoutePrefix("api/User")]
    public class UserApiController : ApiController
    {
        [HttpGet]
        [Route("byId")]
        public async Task<IHttpActionResult> getById(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceUsers service = new ServiceUser();

                Users asset = await service.getById(id);

                if (asset == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Usuario no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Usuario encontrado";
                    response.Data = asset;
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

        [HttpGet]
        public async Task<IHttpActionResult> getAll()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceUsers service = new ServiceUser();

                IEnumerable<Users> Users = await service.getAll();

                if (Users == null || !Users.Any())
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Usuario no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Usuario encontrado";
                    response.Data = Users;
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

        [HttpGet]
        [Route("userGroup")]

        public async Task<IHttpActionResult> getGropsByUser(int userId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceUsers service = new ServiceUser();

                IEnumerable<GroupT> Users = await service.getGropsByUser(userId);

                if (Users == null || !Users.Any())
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Grupos no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Grupos encontrados";
                    response.Data = Users;
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

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(LoginModel user)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceUsers service = new ServiceUser();

                Users Users = await service.Login(user.Email, user.Password);

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
