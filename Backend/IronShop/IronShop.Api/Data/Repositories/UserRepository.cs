using Dapper;
using IronShop.Api.Core.Common;
using IronShop.Api.Core.Entities;
using IronShop.Api.Core.Entities.Base;
using IronShop.Api.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
                                .Skip((pageParameter.PageNumber - 1) * pageParameter.PageSize)
                                .Take(pageParameter.PageSize)
                                .ToListAsync();

            return dataSource;
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
