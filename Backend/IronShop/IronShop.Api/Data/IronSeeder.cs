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

            if (!_context.Role.Any())
            {
                _context.Role.AddRange(
                       GetPreconfiguredRoles());

                await _context.SaveChangesAsync();
            }

            if (!_context.User.Any())
            {
                _context.User.AddRange(
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
                        Description = "Administrator"
                    },
                    new Role()
                    {
                        Description = "User"
                    }
            };
        }

        private List<User> GetPreconfiguredUsers()
        {
            return new List<User>
            {
                new User("Administrator", "admin@fmoralesdev.com","Dfo0NCB4P6Lxfs8MPwFNzYThlMxSEd/uw0cd7ZrH1z4=", Core.Common.eRole.Admin),
                new User("User 1", "user1@fmoralesdev.com","Dfo0NCB4P6Lxfs8MPwFNzYThlMxSEd/uw0cd7ZrH1z4=", Core.Common.eRole.User),
                new User("User 2", "user2@fmoralesdev.com","Dfo0NCB4P6Lxfs8MPwFNzYThlMxSEd/uw0cd7ZrH1z4=", Core.Common.eRole.User),
            };
        }
    }
}
