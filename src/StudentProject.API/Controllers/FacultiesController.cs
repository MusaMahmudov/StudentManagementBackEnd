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
        public async Task<IActionResult> GetAllFaculties([FromQuery]string? search) 
        {
          var Faculties = await _facultyService.GetAllFacultiesAsync(search);
          
            return Ok(Faculties); 
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetFacultyById(Guid Id)
        {
          var Faculty = await _facultyService.GetFacultyByIdAsync(Id);
            return Ok(Faculty);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFaculty(PostFacultyDTO postFacultyDTO)
        {
            await _facultyService.CreateFacultyAsync(postFacultyDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Faculty successefully created"));
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFaculty([FromRoute]Guid Id)
        {
           await _facultyService.DeleteFacultyAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK,"Faculty Deleted"));
        }
        [HttpPost("{Id}")]
        public async Task<IActionResult> UpdateFaculty(Guid Id,PostFacultyDTO postFacultyDTO)
        {
           await _facultyService.UpdateFacultyAsync(Id,postFacultyDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Faculty updated"));


        }
    }
}
