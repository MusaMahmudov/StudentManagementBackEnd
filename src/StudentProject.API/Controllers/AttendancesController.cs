using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.Attendance;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("TeacherPage/{groupSubjectId}")]
        public IActionResult GetAttendancesForTeacherPage(Guid groupSubjectId) 
        {
           var attendances = _attendanceService.GetAttendanceForTeacherPage(groupSubjectId);
            return Ok(attendances);
            
        }
        [HttpGet("{studentId}/{groupSubjectId}")]
        public async Task<IActionResult> GetAttendanceForStudentAttendance(Guid studentId,Guid groupSubjectId)
        {
            var attendances = await _attendanceService.GetAttendanceForStudentAttendanceAsync(studentId, groupSubjectId);
            return Ok(attendances);

        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAttendance(Guid Id,PutAttendanceDTO putAttendanceDTO)
        {
          await  _attendanceService.UpdateAttendanceAsync(Id,putAttendanceDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Update successefully"));
        }

    }
}
