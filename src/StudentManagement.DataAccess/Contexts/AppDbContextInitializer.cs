using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DataAccess.Contexts
{
    public class AppDbContextInitializer
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AppDbContextInitializer(AppDbContext context,RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task InitializerAsync()
        {
           await _context.Database.MigrateAsync();
        }

        public async Task UserSeedAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles))) 
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }

            var admin = new AppUser()
            {
                IsActive = true,
                UserName = "Admin",
                EmailConfirmed = true,
                Email = "musafm@code.edu.az"
            };
           await _userManager.CreateAsync(admin,"Salam123!");
           await  _userManager.AddToRoleAsync(admin,Roles.Admin.ToString());

        }
    }
}
