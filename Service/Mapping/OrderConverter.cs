using AutoMapper;
using Model;
using Model.Responses;

namespace Service.Mapping;

public class OrderConverter : ITypeConverter<Order, OrderResponse>
{
    public OrderResponse Convert(Order source, OrderResponse destination, ResolutionContext context)
    {
        return new(
            source.Id,
            source.User.Id,
            source.OrderDate,
            source.ShippingDate,
            source.Products.Select(p => context.Mapper.Map<OrderProductResponse>(p)).ToArray());
    }
}
