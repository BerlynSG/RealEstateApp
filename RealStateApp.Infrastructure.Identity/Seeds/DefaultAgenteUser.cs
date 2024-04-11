using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultAgenteUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser agenteUser = new()
            {
                UserName = "Agente",
                Email = "agenteuser@gmail.com",
                ImagePath = "",
                FirstName = "Age",
                LastName = "Nte",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != agenteUser.Id))
            {
                var user = await userManager.FindByEmailAsync(agenteUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(agenteUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(agenteUser, Roles.Agente.ToString());
                }
            }

        }
    }
}
