//using System.IO;
//using System.Net;
//using System.Threading.Tasks;
//using ExploringAzureFunctionsApp.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
//using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;

//namespace FunctionApp1FromVs
//{
//    public class zebbi
//    {
//        private readonly ILogger<zebbi> _logger;

//        public zebbi(ILogger<zebbi> log)
//        {
//            _logger = log;
//        }

//        [FunctionName("zebbi")]
 
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] Post reqBody)
//        {
//            //_logger.LogInformation("C# HTTP trigger function processed a request.");

//            //string name = req.Query["name"];

//            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//            //dynamic data = JsonConvert.DeserializeObject(requestBody);
//            //name = name ?? data?.name;

//            //string responseMessage = string.IsNullOrEmpty(name)
//            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
//            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";
//            string responsemessage = "";

//            string Title = reqBody.Title;
//            string Content = reqBody.Content;
//            bool Published = reqBody.Published;

//            //return new OkObjectResult(responseMessage);
//        }
//    }
//}

