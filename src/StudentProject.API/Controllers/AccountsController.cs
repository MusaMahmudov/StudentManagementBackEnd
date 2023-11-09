using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs;
using StudentManagement.Business.DTOs.AuthDTOs;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.UserDTOs;
using StudentManagement.Business.Exceptions.AuthExceptions;
using StudentManagement.Business.Exceptions.UserExceptions;
using StudentManagement.Business.HelperSevices.Interfaces;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.DataAccess.Contexts;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountsController : ControllerBase
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly IUserService _userService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMailService _mailService; 
        public AccountsController(IMailService mailService,IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager,AppDbContext context, IMapper mapper, IUserService userService)
        {
            _mailService = mailService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _userService = userService;
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateUser(PostUserDTO postUserDTO)
        {
            var newUser =   await _userService.CreateAccountAsync(postUserDTO);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            string link = $"http://localhost:3000/ConfirmEmail?token={UrlEncoder.Default.Encode(token)}&email={postUserDTO.Email}";
            var mailRequestDTO = new MailRequestDTO()
            {
                Subject = "Confirm Email",
                ToEmail = postUserDTO.Email,
                Body = $"<h1>Please confirm your email {link} <h1>"
            };
           await _mailService.SendEmail(mailRequestDTO);



            return Ok("Confirm email");

        }
        [HttpPost("[Action]/{token}/{email}")]
        public async Task<IActionResult> ConfirmEmail(string token,string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                return BadRequest("Something went wrong");
            }
            token =System.Web.HttpUtility.UrlDecode(token);

             await _userService.ConfirmEmailAsync(token, email);
            return Ok("Email confirmed");

        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userService.GetAllUsersAsync();
            return Ok(user);
        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Teacher,Student")]
        public async Task<IActionResult> GetUserDetails(string Id)
        {
            var user= await _userService.GetUserByIdAsync(Id);



            return Ok(user);
        }
        [HttpGet("update/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetUserByIdForUpdate(string Id)
        {
            var user = await _userService.GetUserByIdForUpdateAsync(Id);



            return Ok(user);
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> GetUserForStudentAndTeacherUpdate()
        {
            var user = await _userService.GetUsersForTeacherAndStudentUpdateAsync();



            return Ok(user);
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> UdpateUser(string Id,PutUserDTO putUserDTO)
        {
            await _userService.UpdateUserAsync(Id, putUserDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"user updated successefully"));

        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            await _userService.DeleteUserAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "User Deleted successefully"));

        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            if (forgotPasswordDTO.Email is null)
            {
                throw new EmailRequiredException("Email is required");
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            if (user is null)
            {
                throw new UserNotFoundByEmailException("User not found by email");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
           
            token = System.Web.HttpUtility.UrlEncode(token);
            //var token = $"{Guid.NewGuid()}-{user.Id}-{Guid.NewGuid()}";

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "Templates", "reset-password.html");
            string link = $"http://localhost:3000/ResetPassword?token={token}&email={forgotPasswordDTO.Email}";
            StreamReader str = new StreamReader(path);
            var result = await str.ReadToEndAsync();
            var body = result.Replace("[link]", link);


            MailRequestDTO mailRequest = new MailRequestDTO()
            {
                ToEmail = forgotPasswordDTO.Email,
                Subject = "Reset password",
                Body = body,
            };
            await _mailService.SendEmail(mailRequest);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK,token)) ;
        
        }
       
        
        [HttpPost("[Action]")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (string.IsNullOrEmpty(resetPasswordDTO.email) || string.IsNullOrEmpty(resetPasswordDTO.token))
            {
                return BadRequest("Error");
            }
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.email);

            if (user is null)
            {
                throw new UserNotFoundByEmailException("User not found");
            }
            
          
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.token, resetPasswordDTO.Password);
            if(!result.Succeeded)
            {
                throw new ChangePasswordException(result.Errors);
            }
            return Ok("Password changed successefully");

        }

    }
}
