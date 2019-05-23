using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronShop.Api.Core.Entities;

namespace IronShop.Api.Data
{
    public class IronSeeder
    {
        private readonly ApplicationDbContext _context;

        public IronSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            //Verificar si existe BD
            _context.Database.EnsureCreated();

            if (!_context.Roles.Any())
            {
                _context.Roles.AddRange(
                       GetPreconfiguredRoles());

                await _context.SaveChangesAsync();
            }

            if (!_context.Users.Any())
            {
                _context.Users.AddRange(
                       GetPreconfiguredUsers());

                await _context.SaveChangesAsync();
            }


        }

        private List<Role> GetPreconfiguredRoles()
        {
            return new List<Role>
            {
                    new Role()
                    {
                        Description = "ADMIN"
                    },
                    new Role()
                    {
                        Description = "PRODUCT_MANAGER"
                    },
                    new Role()
                    {
                        Description = "SALES_MANAGER"
                    },
                    new Role()
                    {
                        Description = "EMPLOYEE"
                    }
            };
        }

        private List<User> GetPreconfiguredUsers()
        {
            return new List<User>
            {
                new User("Admin", "admin@fmoralesdev.com","Dfo0NCB4P6Lxfs8MPwFNzYThlMxSEd/uw0cd7ZrH1z4=", Core.Common.eRole.Admin),
                new User("Employee", "employee@fmoralesdev.com","Dfo0NCB4P6Lxfs8MPwFNzYThlMxSEd/uw0cd7ZrH1z4=", Core.Common.eRole.Employee),
                new User("Product Manager", "productmanager@fmoralesdev.com","Dfo0NCB4P6Lxfs8MPwFNzYThlMxSEd/uw0cd7ZrH1z4=", Core.Common.eRole.ProductManager),
                new User("Sales Manager", "salesmanager@fmoralesdev.com","Dfo0NCB4P6Lxfs8MPwFNzYThlMxSEd/uw0cd7ZrH1z4=", Core.Common.eRole.SalesManager)
            };
        }
    }
}
