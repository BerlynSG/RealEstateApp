using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultClientUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser clientUser = new()
            {
                UserName = "Cliente",
                Email = "clientuser@email.com",
                ImagePath = "",
                FirstName = "Cli",
                LastName = "Ente",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != clientUser.Id))
            {
                var user = await userManager.FindByEmailAsync(clientUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(clientUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(clientUser, Roles.Cliente.ToString());
                }
            }

        }
    }
}
