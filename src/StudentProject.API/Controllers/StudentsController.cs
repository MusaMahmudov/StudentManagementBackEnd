using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.Exceptions.StudentExceptions;
using StudentManagement.Business.Services.Interfaces;
using System.Net;
using System.Runtime.CompilerServices;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer",Roles ="Admin,Moderator")]

        public async Task<IActionResult> GetStudents(string? search) 
        {
            var students =  await _studentService.GetAllStudentsAsync(search);
            return Ok(students);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateStudent(PostStudentDTO postStudentDTO)
        {
            
              await _studentService.CreateStudentAsync(postStudentDTO);
                return Ok("Student successefully created");
          
        }
        [HttpGet("{Id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Student")]
        public async Task<IActionResult> GetStudentById(Guid Id)
        {
           
                var student = await _studentService.GetStudentByIdAsync(Id);
                return Ok(student);
            
          
       
        }
        [HttpGet("update/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetStudentForUpdate(Guid Id)
        {

            var student = await _studentService.GetStudentByIdForUpdateAsync(Id);
            return Ok(student);

        }
        [HttpGet("[Action]/{Id}")]
        public async Task<IActionResult> GetStudentForStudentPage(Guid Id)
        {
            var student = await _studentService.GetStudentForStudentPageAsync(Id);
            return Ok(student);
        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(Guid Id)
        {
          await  _studentService.DeleteStudentAsync(Id);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Student Deleted Successefully"));
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateStudent(Guid Id,PutStudentDTO putStudentDTO)
        {
           
               await _studentService.UpdateStudentAsync(Id, putStudentDTO);
                return Ok("Success");
          
           

        }
      
    }
}
