using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Model.DTO;
using Service.Interfaces;

namespace API.Controllers;

public class OrderController
{
    private const string ORDERS_QUEUE = "orders";
    private const string ORDERS_TO_PROCESS_QUEUE = "orders-to-process";

    private readonly ILogger _logger;
    private readonly IOrderService _orderService;

    public OrderController(ILoggerFactory loggerFactory, IOrderService orderService)
    {
        _logger = loggerFactory.CreateLogger<OrderController>();
        _orderService = orderService;
    }

    [Function(nameof(PlaceOrder))]
    [QueueOutput(ORDERS_TO_PROCESS_QUEUE)]
    public async Task<ProcessOrderDTO> PlaceOrder(
        [QueueTrigger(ORDERS_QUEUE)] PlaceOrderDTO orderToProcess)
    {
        await _orderService.PlaceOrder(orderToProcess);

        return new ProcessOrderDTO(orderToProcess.OrderId);
    }

    [Function(nameof(ProcessOrder))]
    public async Task ProcessOrder(
        [QueueTrigger(ORDERS_TO_PROCESS_QUEUE)] ProcessOrderDTO orderToProcess)
    {
        await _orderService.ProcessOrder(orderToProcess);
    }
}
