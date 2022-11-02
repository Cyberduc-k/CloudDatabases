using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Model.DTO;
using Model.Responses;
using Service.Interfaces;

namespace API.Controllers;

public class OrderController
{
    private const string ORDERS_QUEUE = "orders";

    private readonly ILogger _logger;
    private readonly IOrderService _orderService;

    public OrderController(ILoggerFactory loggerFactory, IOrderService orderService)
    {
        _logger = loggerFactory.CreateLogger<OrderController>();
        _orderService = orderService;
    }

    [Function(nameof(GetOrders))]
    [OpenApiOperation(nameof(GetOrders), tags: "Orders", Description = "Get all orders")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(OrderResponse[]))]
    public async Task<HttpResponseData> GetOrders(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "orders")] HttpRequestData req)
    {
        ICollection<OrderResponse> orders = await _orderService.GetOrders();
        HttpResponseData resp = req.CreateResponse();

        await resp.WriteAsJsonAsync(orders);

        return resp;
    }

    [Function(nameof(GetOrder))]
    [OpenApiOperation(nameof(GetOrder), tags: "Orders", Description = "Get an order")]
    [OpenApiParameter("orderId", In = ParameterLocation.Path, Type = typeof(Guid), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(OrderResponse))]
    public async Task<HttpResponseData> GetOrder(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "orders/{orderId}")] HttpRequestData req,
        Guid orderId)
    {
        OrderResponse order = await _orderService.GetOrder(orderId);
        HttpResponseData resp = req.CreateResponse();

        await resp.WriteAsJsonAsync(order);

        return resp;
    }

    public struct PlaceOrderOutput
    {
        [QueueOutput(ORDERS_QUEUE)]
        public PlaceOrderDTO PlaceOrderDTO { get; set; }
        public HttpResponseData HttpResponse { get; set; }
    }

    [Function(nameof(PlaceOrder))]
    [OpenApiOperation(nameof(PlaceOrder), tags: "Orders", Description = "Place an order")]
    [OpenApiRequestBody("application/json", typeof(OrderDTO), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(PlaceOrderResponse))]
    public async Task<PlaceOrderOutput> PlaceOrder(
        [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "orders")] HttpRequestData req)
    {
        OrderDTO? orderDto = await req.ReadFromJsonAsync<OrderDTO>();
        Guid orderId = Guid.NewGuid();
        HttpResponseData res = req.CreateResponse(HttpStatusCode.OK);

        await res.WriteAsJsonAsync(new PlaceOrderResponse(orderId));

        return new PlaceOrderOutput {
            PlaceOrderDTO = new PlaceOrderDTO(orderId, orderDto!),
            HttpResponse = res,
        };
    }
}
