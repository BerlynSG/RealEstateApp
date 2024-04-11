using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultDesarrolladorUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser developerUser = new()
            {
                UserName = "Desarrollador",
                Email = "desarrolladoruser@gmail.com",
                ImagePath = "",
                FirstName = "Desa",
                LastName = "Rrollador",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != developerUser.Id))
            {
                var user = await userManager.FindByEmailAsync(developerUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(developerUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(developerUser, Roles.Desarrollador.ToString());
                }
            }

        }
    }
}
