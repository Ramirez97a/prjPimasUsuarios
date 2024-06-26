﻿using ApplicationCore.Services;
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
        [Route("tematicas")]
        public async Task<IHttpActionResult> getAsset(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceGroup service = new ServiceGroup();

                IEnumerable<OrderedTree> tematicas = await service.getTematicByGroup(id);

                if (tematicas == null || !tematicas.Any())
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Temáticas no encontradas verifique el id del grupo ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Temáticas encontradas";
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
                    response.Message = "Temáticas no encontrado.  Verifique el id del grupo.";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Temáticas encontradas";
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
        [HttpGet]
        [Route("getcolorbyID")]
        public async Task<IHttpActionResult> colorbyID(int tematicaId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceGroup service = new ServiceGroup();

                Tematicas tematicas = await service.GetCodColorByTematicaId(tematicaId);
                if (tematicas == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Temática color no encontrada.";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Temáticas encontradas";
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



    }
}
