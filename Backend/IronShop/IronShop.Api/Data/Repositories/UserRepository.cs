using IronShop.Api.Core.Entities;
using IronShop.Api.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.User.AddAsync(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetById(int id)
        {
            return await _context.User.FindAsync(id);
        }
       
    }
}
