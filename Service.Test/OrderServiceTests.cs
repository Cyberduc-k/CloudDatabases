using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Model.Responses;
using Moq;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;
using Xunit;

namespace Service.Test;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepository;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly User _user;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        ServiceProvider services = new ServiceCollection()
            .AddScoped<IImageService, ImageService>()
            .AddScoped<Mapping.OrderConverter>()
            .BuildServiceProvider();

        IMapper mapper = new MapperConfiguration(c => {
            c.ConstructServicesUsing(s => services.GetService(s));
            c.AddMaps(typeof(Mapping.MappingProfile));
        }).CreateMapper();

        _orderRepository = new();
        _userRepository = new();
        _user = new() { Id = Guid.NewGuid() };
        _service = new(_orderRepository.Object, _userRepository.Object, mapper);

        _userRepository.Setup(r => r.Include(u => u.Orders).GetBy(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(_user);
    }

    [Fact]
    public async Task Get_Order_Should_Return_Order()
    {
        Guid orderId = Guid.NewGuid();

        _orderRepository.Setup(r => r.Include(o => o.User)
            .Include(o => o.Products)
            .ThenInclude(o => o.Product)
            .ThenInclude(p => p.Reviews)
            .GetBy(o => o.Id == orderId)
        ).ReturnsAsync(() => new Order() { Id = orderId, User = _user, Products = Array.Empty<OrderProduct>() });

        OrderResponse result = await _service.GetOrder(orderId);

        Assert.Equal(orderId, result.Id);
    }

    [Fact]
    public async Task Get_Order_Should_Throw_Not_Found_Exception()
    {
        _orderRepository.Setup(r => r.Include(o => o.User)
            .Include(o => o.Products)
            .ThenInclude(o => o.Product)
            .ThenInclude(p => p.Reviews)
            .GetBy(It.IsAny<Expression<Func<Order, bool>>>())
        ).ThrowsAsync(new NotFoundException("order"));

        await Assert.ThrowsAsync<NotFoundException>(async () => {
            await _service.GetOrder(Guid.NewGuid());
        });
    }
}
