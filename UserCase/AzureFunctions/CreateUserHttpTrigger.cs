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

namespace UserCase
{
    public class CreateUserHttpTrigger
    {
        private readonly IDbService dbService;

        public CreateUserHttpTrigger(IDbService dbService)
        {
            this.dbService = dbService;
        }

        [FunctionName("CreateUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "CreateUser")] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger CreateUser function processed a request.");

            try
            {
                var watch = Stopwatch.StartNew();

                string requestBody = await req.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<User>(requestBody);

                var newUserId = this.dbService.CreateUser(data);

                watch.Stop();

                var response = new GenericResponse<User>(newUserId, watch.ElapsedMilliseconds);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
