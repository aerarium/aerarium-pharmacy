using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AerariumTech.Pharmacy.Domain;
using static AerariumTech.Pharmacy.Domain.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class IdentityExtensions
    {
        public static async Task<RoleManager<Role>> EnsureCreatedAppRolesAsync(
            this RoleManager<Role> manager)
        {
            var roles = new[] {Pharmacist, Clerk, Manager};
            
            foreach (var role in roles)
            {
                await manager.CreateRoleIfNotExistsAsync(role);
            }

            return manager;
        }

        public static Task<User> FindByCPFAsync(this UserManager<User> manager, string cpf)
        {
            return manager.Users.FirstOrDefaultAsync(u => u.Cpf == cpf);
        }

        public static Task<User> FindByIdAsync(this UserManager<User> manager, long id)
        {
            return manager.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        private static async Task CreateRoleIfNotExistsAsync(this RoleManager<Role> manager,
            string role)
        {
            if (!await manager.RoleExistsAsync(role))
            {
                await manager.CreateAsync(new Role {Name = role});
            }
        }

        private static async Task CreateUserIfNotExistsAsync(this UserManager<User> manager,
            User user, string password)
        {
            if (await manager.FindByEmailAsync(user.Email) == null)
            {
                await manager.CreateAsync(user, password);
            }
        }

        private static async Task CreateUserIfNotExistsAsync(this UserManager<User> manager, User user)
        {
            if (await manager.FindByEmailAsync(user.Email) == null)
            {
                await manager.CreateAsync(user);
            }
        }

        public static async Task<UserManager<User>> EnsureCreatedDevUsersAsync(
            this UserManager<User> manager)
        {
            var users = new List<User>
            {
                new User
                {
                    Name = "Gwydion Pendragon",
                    UserName = "gwyddie",
                    Email = "gwydionp17@gmail.com"
                },
                new User
                {
                    Name = "Cintia Vieira",
                    UserName = "cintiao",
                    Email = "vieiracintia29@gmail.com"
                },
                new User
                {
                    Name = "Priscila Vieira",
                    UserName = "priscilao",
                    Email = "priscila14souza16@gmail.com"
                },
                new User
                {
                    Name = "Daniel Doe",
                    UserName = "dandan",
                    Email = "danielomiya@gmail.com"
                }
            };
            const string password = "admin@123";

            for (var i = 0; i < users.Count; i++)
            {
                var user = users.ElementAtOrDefault(i);
                await manager.CreateAsync(user, password);
                if (i > 0)
                {
                    await manager.AddToRoleAsync(user, i == 1 ? Clerk : i == 2 ? Pharmacist : Manager);
                }
            }

            return manager;
        }
    }
}