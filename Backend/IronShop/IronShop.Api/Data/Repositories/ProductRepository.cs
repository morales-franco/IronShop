using Dapper;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public virtual async Task<IList<T>> GetList<T>(string storedProcedure, KeyValuePair<string, object>[] parameters) where T : class, new()
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            if (parameters != null)
                for (int idx = 0; idx < parameters.Length; idx++)
                    dynamicParameters.Add(parameters[idx].Key, parameters[idx].Value);

            var adoConnection = _context.Database.GetDbConnection().ConnectionString;
            var sqlConnection = new SqlConnection(adoConnection);

            var result = await SqlMapper.QueryAsync<T>(sqlConnection, storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
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
