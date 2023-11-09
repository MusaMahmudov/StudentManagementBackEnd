using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.AuthDTOs;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

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
            if(User.Identity.IsAuthenticated)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseDTO(HttpStatusCode.BadRequest, "Already authenticated"));
            }


            var token  = await _authService.LoginAsync(loginDTO);
            return Ok(token);
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> LogOut()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ResponseDTO(HttpStatusCode.BadRequest, "User is not authenticated"));
            }
            //var link = Url.Action("ResetPassword", "Auth", HttpContext.Request.Scheme);
            await _authService.LogOutAsync();
            //Path.Combine();
            return Ok();

        }
       
        [HttpPut("[Action]/{Id}")]
        public async Task<IActionResult> ChangePassword(string Id ,ChangePasswordDTO changePasswordDTO)
        {
           await _authService.ChangePasswordAsync(Id, changePasswordDTO);
            return Ok("Password changed successefully");
        }

       
    }
}
