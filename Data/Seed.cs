using FabrykaBiznesuV2.Models;
using Microsoft.AspNetCore.Identity;

namespace FabrykaBiznesuV2.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
           



            
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.TeamLeader))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.TeamLeader));
                if (!await roleManager.RoleExistsAsync(UserRoles.ProjectManager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.ProjectManager));

              
                   

             }
        }

    }

   
}