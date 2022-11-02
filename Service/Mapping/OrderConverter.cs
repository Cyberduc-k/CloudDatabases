using AutoMapper;
using Model;
using Model.Responses;

namespace Service.Mapping;

public class OrderConverter : ITypeConverter<Order, OrderResponse>
{
    private readonly IMapper _mapper;

    public OrderConverter(IMapper mapper)
    {
        _mapper = mapper;
    }

    public OrderResponse Convert(Order source, OrderResponse destination, ResolutionContext context)
    {
        return new(
            source.Id,
            source.User.Id,
            source.OrderDate,
            source.ShippingDate,
            source.Products.Select(p => _mapper.Map<OrderProductResponse>(p)).ToArray());
    }
}
