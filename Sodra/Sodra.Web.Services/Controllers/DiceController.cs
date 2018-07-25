using Newtonsoft.Json.Linq;
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
    public class DiceController : ApiController
    {
        [HttpGet]
        [Route("dice/{d:int}/quantity/{n:int}")]
        [ResponseType(typeof(int[]))]
        public HttpResponseMessage RollDice(int  d, int n)
        {
            string errorResponse = "";

            try
            {
                int[] results = new int[n];
                Random rand = new Random();
                for (int i = 0; i < n; ++i)
                    results[i] = rand.Next(1, d);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                JArray JResults = JArray.FromObject(results);

                response.Content = new System.Net.Http.StringContent(JResults.ToString());

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
