using AutoMapper;
using IronShop.Api.Core.Dtos;
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

            CreateMap<Product, ProductDto>();

            CreateMap<Order, OrderDto>();

            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
