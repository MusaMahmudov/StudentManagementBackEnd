using StudentManagement.Business.DTOs.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IAttendanceService
    {
        List<GetAttendanceForTeacherPageDTO> GetAttendanceForTeacherPage(Guid groupSubjectId);
        Task UpdateAttendanceAsync(Guid Id,PutAttendanceDTO putAttendanceDTO);
        Task<List<GetAttendanceForStudentAttendancePageDTO>> GetAttendanceForStudentAttendanceAsync(Guid studentId,Guid groupSubjectId);
    }
}
