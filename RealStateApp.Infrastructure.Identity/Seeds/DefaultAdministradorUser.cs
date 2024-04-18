using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultAdministradorUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser adminUser = new()
            {
                UserName = "Administrador",
                Email = "administradoruser@gmail.com",
                ImagePath = "",
                FirstName = "Adminis",
                LastName = "Trador",
                Cedula = "1010101010",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Rol = 3
            };
            if (userManager.Users.All(u => u.Id != adminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(adminUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(adminUser, Roles.Administrador.ToString());
                }
            }

        }
    }
}
