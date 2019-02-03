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
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Product.FindAsync(id);
        }

        public void Remove(Product product)
        {
            _context.Set<Product>().Remove(product);
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }
    }
}
