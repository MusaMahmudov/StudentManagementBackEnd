using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.FacultyDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        public FacultiesController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetAllFaculties([FromQuery]string? search) 
        {
          var Faculties = await _facultyService.GetAllFacultiesAsync(search);
          
            return Ok(Faculties); 
        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetFacultyById(Guid Id)
        {
          var Faculty = await _facultyService.GetFacultyByIdAsync(Id);
            return Ok(Faculty);
        }
        [HttpGet("update/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetFacultyByIdForUpdate(Guid Id)
        {
            var Faculty = await _facultyService.GetFacultyByIdForUpdateAsync(Id);
            return Ok(Faculty);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateFaculty(PostFacultyDTO postFacultyDTO)
        {
            await _facultyService.CreateFacultyAsync(postFacultyDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Faculty successefully created"));
        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteFaculty([FromRoute]Guid Id)
        {
           await _facultyService.DeleteFacultyAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK,"Faculty Deleted"));
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateFaculty(Guid Id,PutFacultyDTO putFacultyDTO)
        {
           await _facultyService.UpdateFacultyAsync(Id, putFacultyDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Faculty updated"));


        }
    }
}
