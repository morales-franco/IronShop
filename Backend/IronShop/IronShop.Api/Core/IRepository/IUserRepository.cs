using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<PaginableList<User>> GetPagedList(PageParameters<User> pageParameter);
        Task<User> GetByEmail(string email);
        Task<User> GetById(int id);
        void Add(User user);
        void Update(User user);
        void Remove(User user);
    }
}
