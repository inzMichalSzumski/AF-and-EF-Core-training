using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Diagnostics;
using UserCase.Services;
using UserCase.Models;

namespace UserCase.AzureFunctions
{
    public class UpdateUserHttpTrigger
    {
        private readonly IDbService dbService;

        public UpdateUserHttpTrigger(IDbService dbService)
        {
            this.dbService = dbService;
        }
        [FunctionName("UpdateUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "UpdateUser")] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger GetUser function processed a request.");

            try
            {
                var watch = Stopwatch.StartNew();

                string requestBody = await req.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<User>(requestBody);

                var user = this.dbService.UpdateUser(data);

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