using IronShop.Api.Core.Dtos;
using IronShop.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.IServices
{
    public interface IOrderService
    {
        Task<Order> Create(Order order);
        Task<Order> GetById(int orderId);
        Task<IEnumerable<Order>> GetAll();
    }
}
