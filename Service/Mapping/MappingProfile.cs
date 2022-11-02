using AutoMapper;
using Model;
using Model.Responses;

namespace Service.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductResponse>().ConvertUsing<ProductConverter>();
        CreateMap<Order, OrderResponse>().ConvertUsing<OrderConverter>();
        CreateMap<OrderProduct, OrderProductResponse>().ConvertUsing<OrderProductConverter>();
        CreateMap<User, UserResponse>();
    }
}
