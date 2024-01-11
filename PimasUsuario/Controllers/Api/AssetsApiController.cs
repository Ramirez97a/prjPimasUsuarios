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

    [RoutePrefix("api/Assets")]
    public class AssetsApiController : ApiController
    {
        [HttpGet]
        [Route("byId")]
        public async Task<IHttpActionResult> getAsset(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceAssets service = new ServiceAssets();

                Assets asset = await service.getAsset(id);

                if (asset == null )
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Asset no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Asset encontrado";
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
        [Route("byTematic")]
        public async Task<IHttpActionResult> getByTematic(int tematicId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceAssets service = new ServiceAssets();

                IEnumerable< Assets> asset = await service.getByTematic(tematicId);

                if (asset == null || !asset.Any())
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Asset no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Asset encontrado";
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
        [Route("bySubTematic")]
        public async Task<IHttpActionResult> getAllSubtematic(int PtamaticId, int tematicId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceAssets service = new ServiceAssets();

                IEnumerable<Assets> asset = await service.getByTematic(tematicId);

                if (asset == null || !asset.Any())
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Asset no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Asset encontrado";
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
        [Route("getByGroup")]
        public async Task<IHttpActionResult> getByGroup(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceAssets service = new ServiceAssets();

                IEnumerable<Assets> asset = await service.getByGroup(id);             

                if (asset == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Asset no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Asset encontrado";
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
        [Route("getContend")]
        public async Task<IHttpActionResult> getContend(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceAssets service = new ServiceAssets();

              byte[] asset = await service.getContend(id);

                if (asset == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Asset no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Asset encontrado";
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
       
    }
}
