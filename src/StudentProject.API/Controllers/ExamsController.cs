using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.ExamDTOs;
using StudentManagement.Business.Services.Implementations;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _examService;
        public ExamsController(IExamService examService)
        {
            _examService = examService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetAllExams(string? Search)
        {
            var exams = await _examService.GetAllExamsAsync(Search);
            return Ok(exams);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateExam(PostExamDTO postExamDTO)
        {
            await _examService.CreateExamAsync(postExamDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam created successefully"));
        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetExamById(Guid Id)
        {
            var exam = await _examService.GetExamByIdAsync(Id);
            return Ok(exam);

        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> GetAllExamsForExamResultUpdate()
        {
            var exams = await _examService.GetAllExamsForExamResultUpdateAsync();
            return Ok(exams);

        }
        [HttpGet("update/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetExamByIdForUpdate(Guid Id)
        {
            var exam = await _examService.GetExamByIdForUpdateAsync(Id);
            return Ok(exam);

        }
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetExamForExamsForTeacherPageAssign(Guid Id)
        {
            var exam = await _examService.GetExamForExamsForTeacherPageAssignAsync(Id);
            return Ok(exam);
        }
        [HttpGet("[action]/{groupSubjectId}")]
        public async Task<IActionResult> GetExamForSubjectsForStudentPage(Guid groupSubjectId)
        {
            var exams= await _examService.GetExamsForSubjectsForStudentPageAsync(groupSubjectId);
            return Ok(exams);
        }
        [HttpGet("[Action]/{studentId}")]
        public async Task<IActionResult> GetExamsForExamScheduleForStudentPage(Guid studentId)
        {
            var exams = await _examService.GetExamsForExamScheduleForStudentPageAsync(studentId);
            return Ok(exams);
        }
        [HttpGet("[Action]/{teacherId}")]
        public async Task<IActionResult> GetExamsForExamScheduleForTeacherPage(Guid teacherId)
        {
            var exams = await _examService.GetExamsForExamScheduleForTeacherPageAsync(teacherId);
            return Ok(exams);
        }

        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateExam(Guid Id, PutExamDTO putExamDTO)
        {
            await _examService.UpdateExamAsync(Id, putExamDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam updated successefully"));

        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteExam(Guid Id) 
        {
         await _examService.DeleteExamAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam deleted successefully"));

        }
    }
}
