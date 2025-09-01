using Microsoft.AspNetCore.Identity;

namespace GrothPlan_Month_6_Task.code
{
    

    public static class AddRole
    {
        public static async Task AddRolesAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

}
