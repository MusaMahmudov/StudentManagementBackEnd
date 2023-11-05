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

namespace StudentManagement.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IStudentRepository _studentRepository;
       private readonly ITeacherRepository _teacherRepository;
        private readonly IGetEmailTemplate _getEmailTemplate;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        public AuthService(IMapper mapper,ITeacherRepository teacherRepository,IStudentRepository studentRepository,IMailService mailService,IGetEmailTemplate getEmailTemplate,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _mailService = mailService;
            _getEmailTemplate = getEmailTemplate;
            _teacherRepository = teacherRepository;
     
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



            var token = await _tokenService.CreateToken(user,studentDTO,teacherDTO);
            
            return  token;

        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();

        }

        public async Task ResetPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            if(forgotPasswordDTO.Email is null)
            {
                throw new EmailRequiredException("Email is required");
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            if (user is null) 
            {
                throw new UserNotFoundByEmailException("User not found by email");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var body = await _getEmailTemplate.GetResetPasswordTemplateAsync(token,forgotPasswordDTO.Email);
            MailRequestDTO mailRequest = new MailRequestDTO()
            {
                ToEmail = forgotPasswordDTO.Email,
                Subject = "Reset password",
                Body = body,
            };
            await _mailService.SendEmail(mailRequest);


        }
    }

   
    
}
