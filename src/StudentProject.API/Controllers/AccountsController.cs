using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.UserDTOs;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.DataAccess.Contexts;

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
        public AccountsController( AppDbContext context, IMapper mapper,IUserService userService)
        {
            _userService = userService;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(PostUserDTO postUserDTO)
        {
           await _userService.CreateAccountAsync(postUserDTO);
            return Ok("User created successefully");

        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
          var users = await _userService.GetAllUsersAsync();
          


          return Ok(users);
        }
    }
}
