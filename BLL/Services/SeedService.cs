using Abstraction.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    // сервис для сидера
    public class SeedService : ISeedService
    {
        private readonly RoleManager<AppRole> roleManager; // менеджер для работы с ролями
        private readonly UserManager<AppUser> userManager; // менеджер для работы с пользователями

        public SeedService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task SeedUsersAndRolesAsync()
        {
            string[] roleNames = { "User", "Admin", "Manager" }; // наши роли

            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role)) // проверяем если роль уже существует
                {
                    await roleManager.CreateAsync(new AppRole { Name = role, NormalizedName = role.ToUpper() }); // добавляем роль
                }
            }

            var users = new[]
            {   // наши seed данные для пользователей 
                new { Email = "user@example.com", UserName = "user", Password = "User123!", Role = "User" },
                new { Email = "admin@example.com", UserName = "admin", Password = "Admin123!", Role = "Admin" },
                new { Email = "manager@example.com", UserName = "manager", Password = "Manager123!", Role = "Manager" }
            };

            foreach (var userData in users) 
            {
                var existingUser = await userManager.FindByEmailAsync(userData.Email);

                if (existingUser == null)
                {
                    var user = new AppUser
                    {   // создали пользователя по модели AppUser используя наши данные
                        UserName = userData.UserName,
                        Email = userData.Email,
                        EmailConfirmed = true
                    }; 

                    var result = await userManager.CreateAsync(user, userData.Password); // добавляем к нему пароль
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, userData.Role); // добавляем ему роль
                    }

                }
            }
        }
    }
}
