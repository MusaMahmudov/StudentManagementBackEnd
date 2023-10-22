using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetAllSubjects(string? search) 
        {
       var subjects =  await  _subjectService.GetAllSubjectAsync(search);
            return Ok(subjects);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateSubject(PostSubjectDTO postSubjectDTO)
        {
           await _subjectService.CreateSubjectAsync(postSubjectDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Subject created successefully"));

        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteSybject(Guid Id)
        {
           await _subjectService.DeleteSubjectAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Subject deleted successefully"));
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateSubject(Guid Id,PutSubjectDTO putSubjectDTO)
        {
           await _subjectService.UpdateSubjectAsync(Id, putSubjectDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Subject updated successefully"));
        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetSubject(Guid Id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(Id);
            return Ok(subject);
        }
    }
}
