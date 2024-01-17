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
    [RoutePrefix("api/Group")]
    public class GroupApiController : ApiController
    {
        [HttpGet]
        [Route("tematics")]
        public async Task<IHttpActionResult> getAsset(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceGroup service = new ServiceGroup();

                IEnumerable<Tematicas> tematicas = await service.getTematicByGroup(id);

                if (tematicas == null || !tematicas.Any())
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Temanicas no encontrado verifique el id del grupo ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Tematicas encontrados";
                    response.Data = tematicas;
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
        [Route("id")]
        public async Task<IHttpActionResult> getbyId(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceGroup service = new ServiceGroup();

                GroupT group = await service.getbyId(id);

                if (group == null )
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Temanicas no encontrado verifique el id del grupo ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Tematicas encontrados";
                    response.Data = group;
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
