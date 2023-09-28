using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;

namespace StudentManagement.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly 
        private readonly UserManager<AppUser> _userManager;
        public AuthService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task LoginAsync(string userNameOrEmail, string Password)
        {
         var user =  await _userManager.FindByEmailAsync(userNameOrEmail);
            if (user is null) 
            {
                user = await _userManager.FindByNameAsync(userNameOrEmail);
                if(user is null)
                {
                    throw new Exception("UserName/Email or password is wrong");
                }
            }
           
        }
    }
}
