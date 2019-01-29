using IronShop.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmail(string email);
        Task<User> GetById(int id);
        Task Add(User user);
        
    }
}
