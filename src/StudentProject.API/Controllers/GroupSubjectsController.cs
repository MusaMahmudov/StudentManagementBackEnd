using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupSubjectsController : ControllerBase
    {
        private readonly IGroupSubjectService _groupSubjectService;
        public GroupSubjectsController(IGroupSubjectService groupSubjectService)
        {
            _groupSubjectService = groupSubjectService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetAllGroupSubjects()
        {
            var groupSubjects = await _groupSubjectService.GetAllGroupSubjectsAsync();
            return Ok(groupSubjects);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateGroupSubject(PostGroupSubjectDTO postGroupSubjectDTO)
        {
           await _groupSubjectService.CreateGroupSubjectAsync(postGroupSubjectDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Subject for group successefullt created"));

        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetGroupSubjectById(Guid Id)
        {
            var groupSubject = await _groupSubjectService.GetGroupSubjectByIdAsync(Id);
            return Ok(groupSubject);
        }
        [HttpGet("[Action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetGroupSubjectsForExamUpdate()
        {
            var groupSubjects = await _groupSubjectService.GetGroupSubjectsForExamUpdateAsync();
            return Ok(groupSubjects);
        }
        [HttpGet("[Action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetGroupSubjectForUpdate(Guid Id)
        {
            var groupSubject = await _groupSubjectService.GetGroupSubjectForUpdateAsync(Id);
            return Ok(groupSubject);
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateGroupSubject(Guid Id, PutGroupSubjectDTO putGroupSubjectDTO)
        {
           await _groupSubjectService.UpdateGroupSubjectAsync(Id, putGroupSubjectDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK,"Group's subject updated"));
        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteGroupSubject(Guid Id) 
        { 
        await _groupSubjectService.DeleteGroupSubjectAsync(Id);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Group Subject deleted successefully"));
        }

        [HttpGet("[Action]/{teacherId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher")]
        public IActionResult GetGroupSubjectsForTeacherPageDTO(Guid teacherId)
        {
          var groupSubjects =  _groupSubjectService.GetGroupSubjectForTeacherPageDTO(teacherId);
            return Ok(groupSubjects);
        }

        [HttpGet("[Action]/{studentId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Student")]
        public async Task<IActionResult> GetGroupSubjectsForSubjectsForStudentPage(Guid studentId)
        {
            var groupSubjects = await _groupSubjectService.GetGroupSubjectForSubjectsForStudentPageAsync(studentId);
            return Ok(groupSubjects);
        }
    }
}
