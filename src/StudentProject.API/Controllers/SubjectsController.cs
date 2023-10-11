using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.SubjectDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects(string? search) 
        {
       var subjects =  await  _subjectService.GetAllSubjectAsync(search);
            return Ok(subjects);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubject(PostSubjectDTO postSubjectDTO)
        {
           await _subjectService.CreateSubjectAsync(postSubjectDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Subject created successefully"));

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteSybject(Guid Id)
        {
           await _subjectService.DeleteSubjectAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Subject deleted successefully"));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSubject(Guid Id,PutSubjectDTO putSubjectDTO)
        {
           await _subjectService.UpdateSubjectAsync(Id, putSubjectDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Subject updated successefully"));
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetSubject(Guid Id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(Id);
            return Ok(subject);
        }
    }
}
