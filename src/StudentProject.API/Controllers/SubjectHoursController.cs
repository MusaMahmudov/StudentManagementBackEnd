﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.SubjectHourDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateSubjectHours(PostSubjectHourDTO postSubjectHourDTO)
        {
            await _subjectHourService.CreateSubjectHoursAsync(postSubjectHourDTO);
            return Ok("Subject Hours Created");
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetSubjectHours(Guid? groupSubjectId)
        {
          var subjectHours = await _subjectHourService.GetSubjectHourAsync(groupSubjectId);
            return Ok(subjectHours);
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateSubjectHours(Guid Id, PutSubjectHourDTO putSubjectHourDTO)
        {
           await _subjectHourService.UpdateSubjeectHoursAsync(Id, putSubjectHourDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Subject Hours Updated successefyllu"));
        }
        [HttpGet("update/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetSubjectHourForUpdate(Guid Id)
        {
            var subjectHour = await _subjectHourService.GetSubjectHourForUpdateAsync(Id);
            return Ok(subjectHour);
        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetSubjectHourById(Guid Id)
        {
            var subjectHour = await _subjectHourService.GetSubjectHourByIdAsync(Id);
            return Ok(subjectHour);
        }
        [HttpGet("StudentSchedule/{studentId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Student")]
        public async Task<IActionResult> GetSubjectHourForStudentSchedule(Guid studentId)
        {
            var subjectHours = await _subjectHourService.GetSubjectHoursForStudentScheduleAsync(studentId);
            return Ok(subjectHours);
        }
        [HttpGet("TeacherSchedule/{teacherId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher")]
        public async Task<IActionResult> GetSubjectHourForTeacherSchedule(Guid teacherId)
        {
            var subjectHours = await _subjectHourService.GetSubjectHoursForTeacherScheduleAsync(teacherId);
            return Ok(subjectHours);
        }
        [HttpGet("TeacherPage/{groupSubjectId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Teacher")]
        public async Task<IActionResult> GetSubjectHoursForAttendanceForTeacherPage(Guid groupSubjectId)
        {
            var subjectHours = await _subjectHourService.GetSubjectHoursForAttendanceForTeacherPageAsync(groupSubjectId);
            return Ok(subjectHours);
        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteSubjectHour(Guid Id)
        {
          await  _subjectHourService.DeleteSubjectHoursAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Subject Hours Deleted successefyllu"));

        }
       

    }
}
