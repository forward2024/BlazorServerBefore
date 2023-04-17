using Before.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Before.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "doreneto24122412@gmail.com";
            string password = "12345678zZ!";
            string[] roleNames = { "Admin", "User", "Seller" }; // Создайте список ролей, которые вы хотите добавить

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (!userManager.Users.Any())
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                else
                {
                    throw new InvalidOperationException("Не удалось создать администратора с предоставленными данными.");
                }
            }
        }
    }
}