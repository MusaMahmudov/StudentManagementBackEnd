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
        public async Task<IActionResult> GetAllExamResults(string? studentName)
        {
            var examResults = await _examResultService.GetAllExamResultsAsync(studentName);
            return Ok(examResults);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetExamResult(Guid Id)
        {
            var examResult = await _examResultService.GetExamResultByIdAsync(Id);
            return Ok(examResult);
        }


        [HttpPost]
        public async Task<IActionResult> CreateExamResult(PostExamResultDTO postExamResultDTO)
        {
            await _examResultService.CreateExamResultAsync(postExamResultDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam result created successefully"));

        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateExamResult(Guid Id, PutExamResultDTO putExamResultDTO)
        {
        

           await _examResultService.UpdateExamResultAsync(Id, putExamResultDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam result updated successefully"));

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteExamResult(Guid Id)
        {
            await _examResultService.DeleteExamResultAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam result deleted successefully"));

        }
    }
}
