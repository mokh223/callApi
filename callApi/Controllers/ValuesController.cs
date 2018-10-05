using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using callApi.Request;
using callApi.Helpers;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft;

namespace callApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> postNoBearerAsync(string email, string password,string baseUrl, string action)
        {
            var request = new LoginRequest
            {
                email = email,
                password = password
            };

            var callApi = new CallApi(baseUrl);
            var client = callApi.getClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(action, request);
            if (response.IsSuccessStatusCode)
                return Ok(await response.Content.ReadAsAsync<string>());
            else
                return NotFound();
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> getUseBearerAsync(string token, string baseUrl, string action)
        {
            var callApi = new CallApi(baseUrl);
            var client = callApi.getClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(action);
            if (response.IsSuccessStatusCode)
            {
                return Ok(await response.Content.ReadAsStringAsync());

            }
            else
                return NotFound();
        }

    }
}
