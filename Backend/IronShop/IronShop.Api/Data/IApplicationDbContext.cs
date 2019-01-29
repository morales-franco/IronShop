using IronShop.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Data
{
    public interface IApplicationDbContext
    {
         DbSet<User> User { get; set; }
    }
}
