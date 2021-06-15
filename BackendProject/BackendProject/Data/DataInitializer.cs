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
        public DataInitializer(AppDbContext dbContext,UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public void SeeData()
        {
           _dbContext.Database.MigrateAsync();

            #region Roles

            var roles = new List<string>
           {
               Roles.AdminRole,
               Roles.ModeratorRole,
               Roles.MemberRole
           };
            foreach (var role in roles)
            {
                if (_dbContext.Roles.Any(x=> x.Name.ToLower() == role.ToLower()))
                    continue;
                _dbContext.Roles.Add(new IdentityRole(role));
                _dbContext.SaveChanges();
            }

            #endregion

            var user = new User
            {
                Email = "cafarmm@code.edu.az",
                UserName = "admin",
                Fullname = "Cefer Cefer",

            };
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, "Admin12345");
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
