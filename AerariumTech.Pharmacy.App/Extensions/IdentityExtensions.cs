using System.Threading.Tasks;
using AerariumTech.Pharmacy.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class IdentityExtensions
    {
        public static async Task<RoleManager<Role>> EnsureCreatedAppRolesAsync(
            this RoleManager<Role> manager)
        {
            var roles = new[] {"Admin"};

            foreach (var role in roles)
            {
                await manager.CreateRoleIfNotExistsAsync(role);
            }

            return manager;
        }

        public static async Task<User> FindByCPFAsync(this UserManager<User> manager, string cpf)
        {
            return await manager.Users.FirstOrDefaultAsync(u => u.Cpf == cpf);
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

        public static async Task<UserManager<User>> EnsureCreatedDevUsersAsync(
            this UserManager<User> manager)
        {
            var users = new[]
            {
                new User
                {
                    UserName = "gwyddie",
                    Email = "gwydionp17@gmail.com"
                },
                new User
                {
                    UserName = "admin",
                    Email = "admin@aerariumtech.com"
                }
            };

            const string password = "admin@123";

            foreach (var user in users)
            {
                await manager.CreateUserIfNotExistsAsync(user, password);
            }

            return manager;
        }
    }
}