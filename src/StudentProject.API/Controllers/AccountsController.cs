using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.UserDTOs;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.DataAccess.Contexts;
using System.Net;
using System.Runtime.InteropServices;

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
        public AccountsController(AppDbContext context, IMapper mapper, IUserService userService)
        {
            _userService = userService;
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateUser(PostUserDTO postUserDTO)
        {
            await _userService.CreateAccountAsync(postUserDTO);
            return Ok("User created successefully");

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


    }
}
