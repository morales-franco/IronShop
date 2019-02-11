using IronShop.Api.Core.Dtos.Index;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<PaginableList<User>> GetAll(PageParameters<User> parameters);
        Task<IList<T>> GetList<T>(string storedProcedure, params KeyValuePair<string, object>[] parameters) where T : class, new();
        Task<User> GetByEmail(string email);
        Task<User> GetById(int id);
        Task Register(User user);
        Task Update(User user);
        Task Delete(User user);
        Task ChangePassword(User user);
        Task<User> Login(User user);
        Task<User> GetCurrentUser();
    }
}
