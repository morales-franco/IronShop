using IronShop.Api.Core;
using IronShop.Api.Core.IRepository;
using IronShop.Api.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IUserRepository Users { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _dbContext = context;
            Users = new UserRepository(context);

        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
