using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Infrastructure.Identity.Entities;
using RealEstateApp.Infrastructure.Identity.Seeds;

namespace RealEstateApp.Infrastructure.Identity
{
    public static class ServiceApplication
    {
        public static async Task AddIdentitySeeds(this IServiceProvider services)
        {
            #region "Identity seeds"
            using (var scope = services.CreateScope())
            {
                var serviceScope = scope.ServiceProvider;

                try
                {
                    var userManager = serviceScope.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = serviceScope.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(roleManager);  
                    await DefaultClientUser.SeedAsync(userManager);
                    await DefaultAgenteUser.SeedAsync(userManager);
                    await DefaultAdministradorUser.SeedAsync(userManager);
                }
                catch (Exception ex)
                {

                }
            }
            #endregion
        }
    }
}
