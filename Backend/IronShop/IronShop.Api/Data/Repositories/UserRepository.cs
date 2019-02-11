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

        public virtual async Task<IList<T>> GetList<T>(string storedProcedure, params KeyValuePair<string, object>[] parameters) where T : class, new()
        {
            string paramzz = string.Empty;
            DynamicParameters dynamicParameters = new DynamicParameters();
            //object p1 = "iron";
            //object p2 = "test";
            //object p3 = "admin";
            //dParameters.Add("@fullName", p1);
            //dParameters.Add("@email", p2);
            //dParameters.Add("@role", p3);
            Dictionary<string, object> paa = new Dictionary<string, object>();
            //paa.Add("fullName", "iron");

            if (parameters != null)
            {
                var key = parameters[0].Key;
                var value = parameters[0].Value;

                for (int idx = 0; idx < parameters.Length; idx++)
                {
                    dynamicParameters.Add(parameters[idx].Key, parameters[idx].Value);
                }
                paa.Add(key, value.ToString());

            }

            var adoConnection = _context.Database.GetDbConnection().ConnectionString;
            var sqlConnection = new SqlConnection(adoConnection);

            try
            {

                //var result = await SqlMapper.QueryAsync<T>(sqlConnection, storedProcedure, dParameters, commandType: CommandType.StoredProcedure);
                var result = await SqlMapper.QueryAsync<T>(sqlConnection, "IndexUser", paa, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        string ParametersToString(DynamicParameters parameters)
        {
            var result = new StringBuilder();

            if (parameters != null)
            {
                var firstParam = true;
                var parametersLookup = (SqlMapper.IParameterLookup)parameters;
                foreach (var paramName in parameters.ParameterNames)
                {
                    if (!firstParam)
                    {
                        result.Append(", ");
                    }
                    firstParam = false;

                    result.Append('@');
                    result.Append(paramName);
                    result.Append(" = ");
                    try
                    {
                        var value = parametersLookup[paramName];// parameters.Get<dynamic>(paramName);
                        result.Append((value != null) ? value.ToString() : "{null}");
                    }
                    catch
                    {
                        result.Append("unknown");
                    }
                }

            }
            return result.ToString();
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
