using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ExploringAzureFunctionsApp.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;

namespace FunctionApp1FromVs
{
    public static class Addefunc
    {
        [FunctionName("Addefunc")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Create post" })]
        [OpenApiRequestBody("application/json", typeof(Post),
            Description = "JSON request body containing { hours, capacity}")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string),
            Description = "The OK response message containing a JSON result.")]
        public static async Task<IActionResult> Run( [HttpTrigger(AuthorizationLevel.Anonymous, "post")] Post body,
             [CosmosDB(
             databaseName: "ExploringAzureDBTest",
             collectionName: "ExploringAzureContainerTest",
             ConnectionStringSetting = "CosmosDbConnectionString")]
             IAsyncCollector<dynamic> documentsOut)
        {
            string id = body.Id;
            string title = body.Title;
            string content = body.Content;
            bool published = body.Published; 
           

            Post postToCreate = new()
            {
                // Create a random ID.
                Id = Guid.NewGuid().ToString(),
                Title = title,
                Content = content,
                Published = published
            };


            string responseMessage = "Function triggered successfully. If the data was clean, the post has been created.";
            await documentsOut.AddAsync(postToCreate);
            return new OkObjectResult(responseMessage);

        }
    }
}
