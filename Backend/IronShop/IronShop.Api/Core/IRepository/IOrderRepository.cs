using IronShop.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> GetById(int id);
        void Add(Order orderDto);
        Task<long> GetMaxOrderNumber();
        Task<IEnumerable<Order>> GetAll();
    }
}
