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
    public class PlayerController : ApiController
    {
        [HttpGet]
        [Route("player/{name}/sessions")]
        [ResponseType(typeof(List<DTO.Session>))]
        public HttpResponseMessage GetSessions(string name)
        {
            string errorResponse = "";

            try
            {
                List<DTO.Session> sessions = new List<DTO.Session>();
                foreach(var s in RepositoryProxy.Instance.SessionRepository.GetPlayerSessions(name))
                {
                    sessions.Add(SessionConverter.DomainToDTO(s));
                }

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                JArray JSessions = JArray.FromObject(sessions);

                response.Content = new System.Net.Http.StringContent(JSessions.ToString());

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
