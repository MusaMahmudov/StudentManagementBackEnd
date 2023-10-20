using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.SubjectHourDTOs;
using StudentManagement.Business.Services.Interfaces;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectHoursController : ControllerBase
    {
        private readonly ISubjectHourService _subjectHourService;
        public SubjectHoursController(ISubjectHourService subjectHourService)
        {
            _subjectHourService = subjectHourService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubjectHours(PostSubjectHourDTO postSubjectHourDTO)
        {
           await _subjectHourService.CreateSubjectHoursAsync(postSubjectHourDTO);
            return Ok();
        }
    }
}
