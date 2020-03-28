using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using UserCase.Models;
using UserCase.Services;

namespace UserCase.AzureFunctions
{
    public class GetUserHttpTrigger
    {
        private readonly IDbService dbService;

        public GetUserHttpTrigger(IDbService dbService)
        {
            this.dbService = dbService;
        }
        [FunctionName("GetUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "GetUser")] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger GetUser function processed a request.");

            try
            {
                var watch = Stopwatch.StartNew();

                string requestBody = await req.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<User>(requestBody);

                var user = this.dbService.GetUser(data.Id);

                watch.Stop();

                var response = new GenericResponse<User>(user, watch.ElapsedMilliseconds);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}