using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.ExamTypeDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamTypesController : ControllerBase
    {
        private readonly IExamTypeService _examTypeService;
        public ExamTypesController(IExamTypeService service)
        {
            _examTypeService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExamTypes(string? search)
        {
          var examTypes = await _examTypeService.GetAllExamTypesAsync(search);
            return Ok(examTypes);

        }
        [HttpPost]
        public async Task<IActionResult> CreateExamType(PostExamTypeDTO postExamTypeDTO)
        {
            await _examTypeService.CreateExamTypeAsync(postExamTypeDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam type successefully created"));
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetExamTypeById(Guid Id)
        {
          var examType =   await _examTypeService.GetExamTypeByIdAsync(Id);
            return Ok(examType);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteExamTypeAsync(Guid Id)
        {
           await _examTypeService.DeleteExamTypeAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam type successefully deleted "));
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateExamTypeAsync(Guid Id,PostExamTypeDTO postExamTypeDTO)
        {
           await _examTypeService.UpdateExamTypeAsync(Id, postExamTypeDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Exam type successefully updated "));

        }
    }
}
