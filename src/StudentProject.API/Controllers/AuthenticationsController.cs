using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.AuthDTOs;
using StudentManagement.Business.Services.Interfaces;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthenticationsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (User.Identity.IsAuthenticated)
            {

            }


            var token  = await _authService.LoginAsync(loginDTO);
            return Ok(token);
        }
    }
}
