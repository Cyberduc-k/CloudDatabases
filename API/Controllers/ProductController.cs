using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Model.Responses;
using Service.Interfaces;

namespace API.Controllers;

public class ProductController
{
    private readonly ILogger _logger;
    private readonly IProductService _productService;

    public ProductController(ILoggerFactory loggerFactory, IProductService productService)
    {
        _logger = loggerFactory.CreateLogger<ProductController>();
        _productService = productService;
    }

    [Function(nameof(GetProducts))]
    [OpenApiOperation(nameof(GetProducts), tags: "Products", Description = "Get all products")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ProductResponse[]))]
    public async Task<HttpResponseData> GetProducts(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "products")] HttpRequestData req)
    {
        ICollection<ProductResponse> products = await _productService.GetProducts();
        HttpResponseData resp = req.CreateResponse();

        await resp.WriteAsJsonAsync(products);

        return resp;
    }

    [Function(nameof(GetProduct))]
    [OpenApiOperation(nameof(GetProduct), tags: "Products", Description = "Get a product")]
    [OpenApiParameter("productId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ProductResponse))]
    public async Task<HttpResponseData> GetProduct(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "products/{productId}")] HttpRequestData req,
        Guid productId)
    {
        ProductResponse product = await _productService.GetProduct(productId);
        HttpResponseData resp = req.CreateResponse();

        await resp.WriteAsJsonAsync(product);

        return resp;
    }
}
