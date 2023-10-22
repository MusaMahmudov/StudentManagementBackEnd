using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using StudentManagement.Business.Exceptions.AuthExceptions;
using StudentManagement.Business.HelperSevices.Interfaces;
using StudentManagement.Business.DTOs.AuthDTOs;

namespace StudentManagement.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<TokenResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
           

            var user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user is null)
            {
                throw new LoginFailException("Sign in fail");
            }

             var singInResult = await _signInManager.PasswordSignInAsync(user,loginDTO.Password,loginDTO.RememberMe,true);
            if (!singInResult.Succeeded)
            {
                throw new LoginFailException("Sign in fail");
            }


            var token = await _tokenService.CreateToken(user);

            return  token;

        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();

        }
    }

   
    
}
