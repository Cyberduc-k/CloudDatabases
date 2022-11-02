using AutoMapper;
using Model;
using Model.DTO;
using Model.Responses;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    private IIncludableRepository<Order, Review> Query() => _orderRepository
        .Include(o => o.User)
        .Include(o => o.Products)
        .ThenInclude(o => o.Product)
        .ThenInclude(p => p.Reviews);

    public async Task<ICollection<OrderResponse>> GetOrders()
    {
        return await Query().GetAll().Select(o => _mapper.Map<OrderResponse>(o)).ToArrayAsync();
    }

    public async Task<OrderResponse> GetOrder(Guid orderId)
    {
        Order order = await Query().GetBy(o => o.Id == orderId) ?? throw new NotFoundException("order");
        return _mapper.Map<OrderResponse>(order);
    }

    public async Task PlaceOrder(PlaceOrderDTO dto)
    {
        User user = await _userRepository.Include(u => u.Orders).GetBy(u => u.Id == dto.Order.UserId) ?? throw new NotFoundException("user");
        Order order = new(
            dto.OrderId,
            user,
            DateTime.UtcNow,
            null,
            dto.Order.Products.Select(op => {
                return new OrderProduct() { ProductId = op.ProductId, Count = op.Count };
            }).ToArray()
        );

        user.Orders.Add(order);
        await _orderRepository.Insert(order);
        await _orderRepository.SaveChanges();
    }

    public async Task ProcessOrder(ProcessOrderDTO orderToProcess)
    {
        Order order = await _orderRepository.GetById(orderToProcess.OrderId) ?? throw new NotFoundException("order");

        order.ShippingDate = DateTime.UtcNow;
        await _orderRepository.SaveChanges();
    }
}
