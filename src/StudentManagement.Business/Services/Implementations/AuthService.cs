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
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        
        public Task LoginAsync(string userName, string Password)
        {
           
            throw new NotImplementedException();
        }
    }

   
    
}
