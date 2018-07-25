using Newtonsoft.Json.Linq;
using Sodra.SqlClient.Converters;
using Sodra.SqlClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sodra.Web.Services.Controllers
{
    [RoutePrefix("sodra")]
    public class CharacterController : ApiController
    {
        [HttpGet]
        [Route("character/{ID:int}")]
        [ResponseType(typeof(DTO.Character))]
        public HttpResponseMessage Get(int ID)
        {
            string errorResponse = "";

            try
            {
                DTO.Character character = CharacterConverter.DomainToDTO(RepositoryProxy.Instance.CharacterRepository.Get(ID));
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                JObject JCharacter = JObject.FromObject(character);
                response.Content = new System.Net.Http.StringContent(JCharacter.ToString());
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception ex)
            {
                errorResponse = ex.Message;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        [Route("character/edit")]
        [ResponseType(typeof(DTO.Character))]
        public HttpResponseMessage Edit(DTO.Character character)
        {
            string errorResponse = "";

            try
            {
                character = CharacterConverter.DomainToDTO(RepositoryProxy.Instance.CharacterRepository.Edit(character));
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                JObject JCharacter = JObject.FromObject(character);
                response.Content = new System.Net.Http.StringContent(JCharacter.ToString());
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception ex)
            {
                errorResponse = ex.Message;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
