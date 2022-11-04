using AutoMapper;
using Model;
using Model.Responses;

namespace Service.Mapping;

public class OrderProductConverter : ITypeConverter<OrderProduct, OrderProductResponse>
{
    public OrderProductResponse Convert(OrderProduct source, OrderProductResponse destination, ResolutionContext context)
    {
        return new(context.Mapper.Map<ProductResponse>(source.Product), source.Count);
    }
}
