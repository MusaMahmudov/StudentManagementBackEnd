using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.ExamResultDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultsController : ControllerBase
    {
        private readonly IExamResultService _examResultService;
        public ExamResultsController(IExamResultService examResultService)
        {
            _examResultService = examResultService;
        }
        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetAllExamResults(string? studentName)
        {
            var examResults = await _examResultService.GetAllExamResultsAsync(studentName);
            return Ok(examResults);
        }
        [HttpGet("{Id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetExamResult(Guid Id)
        {
            var examResult = await _examResultService.GetExamResultByIdAsync(Id);
            return Ok(examResult);
        }
        [HttpGet("{examId}/{studentId}")]
        public async Task<IActionResult> GetExamResult(Guid examId,Guid studentId)
        {
            var examResult = await _examResultService.GetExamResultForExamForStudentPageAsync(examId,studentId); ;
            return Ok(examResult);
        }
        [HttpGet("[Action]/{groupSubjectId}/{studentId}")]
        public async Task<IActionResult> GetExamResultsForFinalExamForStudentPage(Guid groupSubjectId,Guid studentId)
        {
            var examResult = await _examResultService.GetExamResultsForFinalExamForStudentPageAsync( studentId); ;
            return Ok(examResult);
        }
        [HttpGet("update/{examResultId}")]
        public async Task<IActionResult> GetExamResultForUpdate(Guid examResultId)
        {
            var examResult = await _examResultService.GetExamResultForUpdateAsync(examResultId); ;
            return Ok(examResult);
        }


        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateExamResult(PostExamResultDTO postExamResultDTO)
        {
            await _examResultService.CreateExamResultAsync(postExamResultDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam result created successefully"));

        }
        [HttpPut("{Id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateExamResult(Guid Id, PutExamResultDTO putExamResultDTO)
        {
        

           await _examResultService.UpdateExamResultAsync(Id, putExamResultDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam result updated successefully"));

        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteExamResult(Guid Id)
        {
            await _examResultService.DeleteExamResultAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam result deleted successefully"));

        }
    }
}
