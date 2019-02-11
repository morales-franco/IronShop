using IronShop.Api.Core.Common;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;
using IronShop.Api.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IronShop.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.User.Add(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }
        public async Task<PaginableList<User>> GetPagedList(PageParameters<User> pageParameter)
        {
            PaginableList<User> dataSource = new PaginableList<User>();

            IQueryable<User> query = _context.User;

            if (pageParameter.Filter != null)
                query = query.Where(pageParameter.Filter);

            if (pageParameter.Includes != null)
                pageParameter.Includes.ForEach(fk => query = query.Include(fk));

            dataSource.TotalRows = await query.CountAsync();

            if (pageParameter.Sort != null)
                query = pageParameter.Direction == GridDirection.Asc ? query.OrderBy(pageParameter.Sort) : query.OrderByDescending(pageParameter.Sort);

            dataSource.Rows = await query
                                .Skip(pageParameter.PageNumber * pageParameter.RowsPerPage)
                                .Take(pageParameter.RowsPerPage)
                                .ToListAsync();

            return dataSource;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetById(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Remove(User user)
        {
            _context.Set<User>().Remove(user);
        }
    }
}
