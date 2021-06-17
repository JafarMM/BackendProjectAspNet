using BackendProject.DataAccesLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Data
{
    public class DataInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DataInitializer(AppDbContext dbContext,UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedDataAsync()
        {
           await _dbContext.Database.MigrateAsync();

            #region Roles

            var roles = new List<string>
           {
              Roles.AdminRole,
              Roles.ModeratorRole,
              Roles.MemberRole
           };
            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                    continue;

                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            #endregion

            var user = new User
            {
                Email = "cafarmm@code.edu.az",
                UserName = "admin",
                FullName = "Cefer Cefer",

            };
            if(await _userManager.FindByEmailAsync(user.Email) == null)
            {
                await _userManager.CreateAsync(user, "Admin@123");
                await _userManager.AddToRoleAsync(user, Roles.AdminRole);
            }
             
            
        }
    }
}
