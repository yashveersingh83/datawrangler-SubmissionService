using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmissionService.Application.DTOs;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestMetricsController : ControllerBase
    {
        private const int EndpointCallLimit = 1000;
        private const int FailureApiCount = 100;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPublishEndpoint _publishEndpoint;

        public TestMetricsController(IHttpClientFactory httpClientFactory , IPublishEndpoint publishEndpoint)
        {
            _httpClientFactory = httpClientFactory;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("generate-metrics")]
        public async Task<IActionResult> GenerateMetrics()
        {
            var client = _httpClientFactory.CreateClient();
            var baseUrl = "http://localhost:8010"; // Base URL of your service
            var endpoints = new[]
            {
                "/api/lookup/request-status",
                "/api/lookup/submission-type",
                "/api/manager",
                "/api/milestone",
                "/api/InformationRequest"
            };

            // Retrieve the token from the HttpContext
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing or invalid.");
            }

            await CallGetForAPI_Authorize
                (client, baseUrl, endpoints, token);

            await CallGetForAPI_UnAuthorize
             (client, baseUrl, endpoints, token);

           // await CallGetForAPI_LongWait(client, baseUrl, endpoints, token);

            await PublishMessage(client, baseUrl, endpoints, token);

            return Ok("Metrics generation completed.");
        }

        private static async Task CallGetForAPI_Authorize(HttpClient client, string baseUrl, string[] endpoints, string token)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            foreach (var endpoint in endpoints)
            {
                for (int i = 0; i < EndpointCallLimit; i++) // Call each endpoint 10 times
                {
                    var response = await client.GetAsync($"{baseUrl}{endpoint}");
                    var content = await response.Content.ReadAsStringAsync();

                    // Log or process the response if needed
                    Console.WriteLine($"Called {endpoint}: {response.StatusCode}");
                }
            }
        }
        private static async Task CallGetForAPI_UnAuthorize(HttpClient client, string baseUrl, string[] endpoints, string token)
        {
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            foreach (var endpoint in endpoints)
            {
                for (int i = 0; i < FailureApiCount; i++) // Call each endpoint 10 times
                {
                    var response = await client.GetAsync($"{baseUrl}{endpoint}");
                    var content = await response.Content.ReadAsStringAsync();

                    // Log or process the response if needed
                    Console.WriteLine($"Called {endpoint}: {response.StatusCode}");
                }
            }
        }

        private static async Task CallGetForAPI_LongWait(HttpClient client, string baseUrl, string[] endpoints, string token)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            foreach (var endpoint in endpoints)
            {
                for (int i = 0; i < 50; i++) // Call each endpoint 10 times
                {
                    System.Threading.Thread.Sleep(600); // Simulate a long wait
                    var response = await client.GetAsync($"{baseUrl}{endpoint}");
                    var content = await response.Content.ReadAsStringAsync();

                    // Log or process the response if needed
                    Console.WriteLine($"Called {endpoint}: {response.StatusCode}");
                }
            }
        }
        private  async Task PublishMessage(HttpClient client, string baseUrl, string[] endpoints, string token)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            foreach (var endpoint in endpoints)
            {
                for (int i = 0; i < 500; i++) // Call each endpoint 10 times
                {

                    await _publishEndpoint.Publish(new MileStoneDto { Comments = "Sample Message" });
                    // Log or process the response if needed
                    
                }
            }
        }

        
    }
}
