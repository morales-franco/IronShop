using IronShop.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
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
