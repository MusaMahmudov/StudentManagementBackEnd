using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.TeacherRoleDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherRolesController : ControllerBase
    {
        private readonly ITeacherRoleService _teacherRoleService;
        public TeacherRolesController(ITeacherRoleService teacherRoleService)
        {
            _teacherRoleService = teacherRoleService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetAllTeacherRoles(string? search)
        {
        var teacherRoles =  await _teacherRoleService.GetAllTeacherRolesAsync(search);
            return Ok(teacherRoles);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateTeacherRole(PostTeacherRoleDTO postTeacherRoleDTO)
        {
            await _teacherRoleService.CreateTeacherRoleAsync(postTeacherRoleDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Teacher role created"));
        }

        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetTeacherRoleById(Guid Id)
        {
            var teacherRole = await _teacherRoleService.GetTeacherRoleByIdAsync(Id);
            return Ok(teacherRole);
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateTeacherRole(Guid Id, PutTeacherRoleDTO putTeacherRoleDTO)
        {
           await _teacherRoleService.UpdateTeacherRoleAsync(Id,putTeacherRoleDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Teacher role updated"));
        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteTeacherRole(Guid Id)
        {
          await  _teacherRoleService.DeleteTeacherRoleAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Teacher role deleted"));

        }
    }
}
