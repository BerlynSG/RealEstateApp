using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Cliente.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Agente.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Administrador.ToString()));
        }
    }
}
