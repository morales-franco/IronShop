using IronShop.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IList<T>> GetList<T>(string storedProcedure, KeyValuePair<string, object>[] parameters) where T : class, new();
        Task<Product> GetById(int id);
        void Add(Product product);
        void Update(Product product);
    }
}
