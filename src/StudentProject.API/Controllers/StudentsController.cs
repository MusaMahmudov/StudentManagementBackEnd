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
        public async Task<IActionResult> GetStudents(string? search) 
        {
            var students =  await _studentService.GetAllStudentsAsync(search);
            return Ok(students);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(PostStudentDTO postStudentDTO)
        {
            
              await _studentService.CreateStudentAsync(postStudentDTO);
                return Ok("Student successefully created");
          
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStudentById(Guid Id)
        {
           
                var student = await _studentService.GetStudentByIdAsync(Id);
                return Ok(student);
            
          
       
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStudent(Guid Id)
        {
          await  _studentService.DeleteStudentAsync(Id);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Student Deleted Successefully"));
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateStudent(Guid Id,PutStudentDTO putStudentDTO)
        {
           
               await _studentService.UpdateStudentAsync(Id, putStudentDTO);
                return Ok("Success");
          
           

        }
      
    }
}
