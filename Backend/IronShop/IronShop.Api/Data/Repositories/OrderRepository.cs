using IronShop.Api.Core.Entities;
using IronShop.Api.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders
                .Include(m => m.User)
                .Include("Items.Product")
                .ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await _context.Orders
                .Include(m => m.User)
                .Include(m => m.Items.Select(i => i.Product))
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<long> GetMaxOrderNumber()
        {
            long maxOrderNumber = 0;

            if (await _context.Orders.AnyAsync())
            {
                maxOrderNumber = await _context.Orders.MaxAsync(o => o.OrderNumber);
            }

            return maxOrderNumber;
        }
    }
}
