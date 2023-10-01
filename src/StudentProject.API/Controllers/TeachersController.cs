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
        public async Task<IActionResult> GetAllTeachers(string? search) 
        {
          var  teachers = await _teacherService.GetAllTeachersAsync(search);
            return Ok(teachers);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(PostTeacherDTO postTeacherDTO)
        {
          await  _teacherService.CreateTeacherAsync(postTeacherDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Teacher created successefully"));
        }
    }
}
