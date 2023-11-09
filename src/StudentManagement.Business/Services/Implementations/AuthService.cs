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
using StudentManagement.Business.Exceptions.UserExceptions;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StudentManagement.Business.DTOs;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DataAccess.Repositories.Interfaces;
using StudentManagement.Core.Entities;
using AutoMapper;
using StudentManagement.Business.DTOs.TeacherDTOs;
using StudentManagement.Business.DTOs.StudentDTOs;
using Microsoft.AspNetCore.Hosting;
using Org.BouncyCastle.Tls;

namespace StudentManagement.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IStudentRepository _studentRepository;
       private readonly ITeacherRepository _teacherRepository;
        
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthService(IWebHostEnvironment webHost,IHttpContextAccessor contextAccessor,IMapper mapper,ITeacherRepository teacherRepository,IStudentRepository studentRepository,IMailService mailService,IGetEmailTemplate getEmailTemplate,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _studentRepository = studentRepository;
            
            _teacherRepository = teacherRepository;
     
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task ChangePasswordAsync(string id, ChangePasswordDTO changePasswordDTO)
        {
           var user =  await _userManager.FindByIdAsync(id);
            if(user is null)
            {
                throw new UserNotFoundByIdException("User not found");
            }
            if(changePasswordDTO.oldPassword == changePasswordDTO.Password)
            {
                throw new PasswordsAreSameException("Same passwords");
            }


         var result =  await _userManager.CheckPasswordAsync(user, changePasswordDTO.oldPassword);
            if (!result)
            {
                throw new OldPasswordIsNotCorrectException("OldPassword is not correct");   
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resultChange = await _userManager.ResetPasswordAsync(user,token,changePasswordDTO.Password);
            if(!resultChange.Succeeded) 
            {
                throw new ChangePasswordException(resultChange.Errors);
            }
           await _userManager.UpdateAsync(user);
            
               
        }

        public async Task<TokenResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
           if(_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new LoginFailException("Login Fail");
            }
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
            Student? student = await _studentRepository.GetSingleAsync(s => s.AppUserId == user.Id);
            Teacher? teacher = await _teacherRepository.GetSingleAsync(t => t.AppUserId == user.Id);
            TeacherForTokenDTO? teacherDTO = null;
            StudentForTokenDTO? studentDTO = null;

            if (teacher is not null)
            {
               teacherDTO = _mapper.Map<TeacherForTokenDTO>(teacher);
            }
            if (student is not null)
            {
                studentDTO = _mapper.Map<StudentForTokenDTO>(student);
            }



            var token = await _tokenService.CreateToken(user,studentDTO,teacherDTO,loginDTO.RememberMe);
            
            return  token;

        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();

        }

      
      

    }

   
    
}
