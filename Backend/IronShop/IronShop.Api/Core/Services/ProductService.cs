using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;
using IronShop.Api.Core.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public ProductService(IUnitOfWork unitOfWork,
                              IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task Create(Product product)
        {
            await AuditOperation(product);
            _unitOfWork.Products.Add(product);
            await _unitOfWork.Commit();
        }

        private async Task AuditOperation(IAuditable audit)
        {
            var currentUser = await _userService.GetCurrentUser();

            audit.AuditDate = DateTime.Now;
            audit.AuditUserName = currentUser.Email;
        }

        public async Task Delete(Product product)
        {
            product.MarkAsDeleted();
            await Update(product);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _unitOfWork.Products.GetAll();
        }

        public virtual async Task<IList<T>> GetList<T>(string storedProcedure, KeyValuePair<string, object>[] parameters) where T : class, new()
        {
            return await _unitOfWork.Products.GetList<T>(storedProcedure, parameters);
        }

        public async Task<Product> GetById(int id)
        {
            return await _unitOfWork.Products.GetById(id);
        }

        public async Task Update(Product product)
        {
            await AuditOperation(product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Commit();
        }
    }
}
