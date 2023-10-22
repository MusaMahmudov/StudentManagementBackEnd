using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.TeacherDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetAllTeachers(string? search) 
        {
          var  teachers = await _teacherService.GetAllTeachersAsync(search);
            return Ok(teachers);
        }
        [HttpGet("update/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetTeacherForUpdate([FromRoute]Guid Id)
        {
            var teacher = await _teacherService.GetTeacherByIdForUpdate(Id);
            return Ok(teacher);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateTeacher(PostTeacherDTO postTeacherDTO)
        {
          await  _teacherService.CreateTeacherAsync(postTeacherDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Teacher created successefully"));
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateTeacher(Guid Id,PutTeacherDTO putTeacherDTO)
        {
          await  _teacherService.UpdateTeacherAsync(Id, putTeacherDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Teacher updated"));
        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetTeacherById(Guid Id)
        {
        var teacher =  await  _teacherService.GetTeacherByIdAsync(Id);
            return Ok(teacher);
        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteTeacherById(Guid Id)
        {
           await _teacherService.DeleteTeacherAsync(Id);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Teacher deleted successefully"));
        }
    }
}
