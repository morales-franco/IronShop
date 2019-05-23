using AutoMapper;
using IronShop.Api.Core.Dtos;
using IronShop.Api.Core.Dtos.Index;
using IronShop.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.AppStart
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<User, ProfileDto>()
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Role.Description));

            CreateMap<User, UserIndexDto>();

            CreateMap<Product, ProductDto>()
                .ReverseMap();

            CreateMap<Order, OrderDto>()
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ReverseMap();
        }
    }
}
