using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.Attendance;
using StudentManagement.Business.Exceptions.StudentExceptions;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMapper _mapper;
        public AttendanceService(IMapper mapper,IAttendanceRepository attendanceRepository)
        {
            _mapper = mapper;
            _attendanceRepository = attendanceRepository;
        }

        public async Task<List<GetAttendanceForStudentAttendancePageDTO>> GetAttendanceForStudentAttendanceAsync(Guid studentId, Guid groupSubjectId)
        {
           var attendances = await _attendanceRepository.GetFiltered(a=>a.StudentId == studentId && a.SubjectHour.GroupSubjectId == groupSubjectId, "SubjectHour.LessonType").OrderBy(a=>a.SubjectHour.Date ).ThenBy(a=>a.SubjectHour.StartTime).ToListAsync();
            
            var attendancesDTO = _mapper.Map<List<GetAttendanceForStudentAttendancePageDTO>>(attendances);
            return attendancesDTO;
        }

        public List<GetAttendanceForTeacherPageDTO> GetAttendanceForTeacherPage(Guid groupSubjectId)
        {
           var attendances = _attendanceRepository.GetFiltered(a=>a.SubjectHour.GroupSubjectId == groupSubjectId, "Student", "SubjectHour.GroupSubject", "SubjectHour.LessonType");
           
            var attendancesDTO =  _mapper.Map<List<GetAttendanceForTeacherPageDTO>>(attendances);
            return attendancesDTO;

        }

        public async Task UpdateAttendanceAsync(Guid Id,PutAttendanceDTO putAttendanceDTO)
        {
         var attendance = await  _attendanceRepository.GetSingleAsync(a=>a.Id == Id);
            if(attendance is null)
            {
                throw new StudentNotFoundByIdException("Attendance not found");
            }
            attendance = _mapper.Map(putAttendanceDTO, attendance);
            _attendanceRepository.Update(attendance);
          await  _attendanceRepository.SaveChangesAsync();
        }
    }
}
