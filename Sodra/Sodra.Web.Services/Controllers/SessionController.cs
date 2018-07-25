using Newtonsoft.Json.Linq;
using Sodra.SqlClient;
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
    public class SessionController : ApiController
    {
        [HttpPost]
        [Route("session/create")]
        [ResponseType(typeof(DTO.Session))]
        public HttpResponseMessage Create(DTO.Session session)
        {
            string error = "";
            try
            {
                session = SessionConverter.DomainToDTO(RepositoryProxy.Instance.SessionRepository.Add(session));
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                JObject JSession = JObject.FromObject(session);
                response.Content = new System.Net.Http.StringContent(JSession.ToString());
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            HttpResponseMessage errorResponse = new HttpResponseMessage();
            errorResponse.StatusCode = HttpStatusCode.BadRequest;
            errorResponse.ReasonPhrase = error;
            return errorResponse;
        }

        [HttpGet]
        [Route("session/{ID:int}")]
        [ResponseType(typeof(DTO.Session))]
        public HttpResponseMessage Get(int ID)
        {
            string errorResponse = "";

            try
            {
                DTO.Session session = SessionConverter.DomainToDTO(RepositoryProxy.Instance.SessionRepository.Get(ID));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                JObject JSession = JObject.FromObject(session);

                response.Content = new System.Net.Http.StringContent(JSession.ToString());

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
        [Route("session/{ID:int}/join")]
        [ResponseType(typeof(DTO.Session))]
        public HttpResponseMessage Join(int ID, DTO.Character character)
        {
            string errorResponse = "";

            try
            {
                DTO.Session session = SessionConverter.DomainToDTO(RepositoryProxy.Instance.SessionRepository.AddPlayer(ID, character));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                JObject JSession = JObject.FromObject(session);

                response.Content = new System.Net.Http.StringContent(JSession.ToString());

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
        [Route("session/{ID:int}/add_message")]
        [ResponseType(typeof(DTO.Log))]
        public HttpResponseMessage AddMessage(int ID, DTO.Log log)
        {
            string errorResponse = "";

            try
            {
                log = LogConverter.DomainToDTO(RepositoryProxy.Instance.LogRepository.Add(log));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                JObject JLog = JObject.FromObject(log);
                response.Content = new System.Net.Http.StringContent(JLog.ToString());
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
        [Route("session/{ID:int}/change_name/{name}")]
        public HttpResponseMessage ChangeName(int ID, string name)
        {
            string errorResponse = "";

            try
            {
                RepositoryProxy.Instance.SessionRepository.Rename(ID, name);
                
                return new HttpResponseMessage(HttpStatusCode.OK); ;
            }
            catch (Exception ex)
            {
                errorResponse = ex.Message;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        [Route("session/{ID:int}/remove_character")]
        public HttpResponseMessage RemoveCharacter(DTO.Character character)
        {
            string errorResponse = "";

            try
            {
                RepositoryProxy.Instance.CharacterRepository.Delete(character);

                return new HttpResponseMessage(HttpStatusCode.OK); ;
            }
            catch (Exception ex)
            {
                errorResponse = ex.Message;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
