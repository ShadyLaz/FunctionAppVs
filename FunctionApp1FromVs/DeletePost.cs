using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ExploringAzureFunctionsApp;

public class DeletePost
{
    private readonly ILogger<DeletePost> _logger;

    public DeletePost(ILogger<DeletePost> log)
    {
        _logger = log;
    }

    [FunctionName("DeletePost")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Delete post" })]
    [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **ID** parameter")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)]
        HttpRequest httpRequest,
        [CosmosDB(
            databaseName: "ExploringAzureDBTest",
            collectionName: "ExploringAzureContainerTest",
            Id = "{Query.id}",
            PartitionKey = "{Query.id}",
            ConnectionStringSetting = "CosmosDbConnectionString")] Document document,
        [CosmosDB(
            databaseName: "ExploringAzureDBTest",
            collectionName: "ExploringAzureContainerTest",
            ConnectionStringSetting = "CosmosDbConnectionString")] DocumentClient client
        )
    {
        _logger.LogInformation($"C# HTTP trigger function processed a request. Function name: {nameof(DeletePost)}");

        string id = httpRequest.Query["id"];

        if (document == null || string.IsNullOrEmpty(id))
        {
            return new BadRequestResult();
        }

        await client.DeleteDocumentAsync(document.SelfLink, new RequestOptions() { PartitionKey = new PartitionKey(id) });

        return new OkResult();
    }
}
