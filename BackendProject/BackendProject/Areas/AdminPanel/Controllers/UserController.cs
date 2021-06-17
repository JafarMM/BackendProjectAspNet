using BackendProject.Models;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    //[Authorize(Roles = Roles.AdminRole)]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async  Task<IActionResult>Index()
        {
            var dbUsers = await _userManager.Users.ToListAsync();
            var users = new List<UserViewModel>();

            foreach (var dbUser in dbUsers)
            {
                var user = new UserViewModel
                {
                    Id = dbUser.Id,
                    Username = dbUser.UserName,
                    Email = dbUser.Email,
                    Fullname = dbUser.FullName,
                    Role = (await _userManager.GetRolesAsync(dbUser)).FirstOrDefault()
                    
                };
                users.Add(user);
            }

            return View(users);
        }
    }
}
