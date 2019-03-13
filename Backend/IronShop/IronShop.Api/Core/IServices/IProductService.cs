﻿using IronShop.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IList<T>> GetList<T>(string storedProcedure, params KeyValuePair<string, object>[] parameters) where T : class, new();
        Task<Product> GetById(int id);
        Task Update(Product product);
        Task Delete(Product product);
        Task Create(Product productEntity);
    }
}
