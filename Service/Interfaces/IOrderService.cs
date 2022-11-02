using Model.DTO;
using Model.Responses;

namespace Service.Interfaces;

public interface IOrderService
{
    public Task<ICollection<OrderResponse>> GetOrders();
    public Task<OrderResponse> GetOrder(Guid orderId);

    public Task PlaceOrder(PlaceOrderDTO orderDto);
    public Task ProcessOrder(ProcessOrderDTO orderToProcess);
}
