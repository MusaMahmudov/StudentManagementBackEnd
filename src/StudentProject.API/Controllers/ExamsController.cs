using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.ExamDTOs;
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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllExams(string? Search)
        {
            var exams = await _examService.GetAllExamsAsync(Search);
            return Ok(exams);
        }
        [HttpPost]
        public async Task<IActionResult> CreateExam(PostExamDTO postExamDTO)
        {
            await _examService.CreateExamAsync(postExamDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam created successefully"));
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetExamById(Guid Id)
        {
            var exam = await _examService.GetExamByIdAsync(Id);
            return Ok(exam);

        }
        [HttpGet("update/{Id}")]
        public async Task<IActionResult> GetExamByIdForUpdate(Guid Id)
        {
            var exam = await _examService.GetExamByIdForUpdateAsync(Id);
            return Ok(exam);

        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateExam(Guid Id, PutExamDTO putExamDTO)
        {
            await _examService.UpdateExamAsync(Id, putExamDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam updated successefully"));

        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteExam(Guid Id) 
        {
         await _examService.DeleteExamAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam deleted successefully"));

        }
    }
}
