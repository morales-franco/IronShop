using IronShop.Api.Core.Entities;
using IronShop.Api.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Product.Add(product);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Product
                .Where(p => !p.Deleted)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Product.FirstOrDefaultAsync(p => !p.Deleted && p.ProductId == id);
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }
    }
}
