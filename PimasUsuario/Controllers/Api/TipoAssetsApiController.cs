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
    [RoutePrefix("api/TypeAssets")]
    public class TipoAssetsApiController : ApiController
    {

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> getByTematic(int tematicId, int group)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceTipoAssets service = new ServiceTipoAssets();

                IEnumerable<TipoAssets> types = await service.getAll();

                if (types == null || !types.Any())
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Tipos no encontrados ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Datos encontrados";
                    response.Data = types;
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
