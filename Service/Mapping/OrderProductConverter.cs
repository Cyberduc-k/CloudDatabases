using AutoMapper;
using Model;
using Model.Responses;

namespace Service.Mapping;

public class OrderProductConverter : ITypeConverter<OrderProduct, OrderProductResponse>
{
    private readonly IMapper _mapper;

    public OrderProductConverter(IMapper mapper)
    {
        _mapper = mapper;
    }

    public OrderProductResponse Convert(OrderProduct source, OrderProductResponse destination, ResolutionContext context)
    {
        return new(_mapper.Map<ProductResponse>(source.Product), source.Count);
    }
}
