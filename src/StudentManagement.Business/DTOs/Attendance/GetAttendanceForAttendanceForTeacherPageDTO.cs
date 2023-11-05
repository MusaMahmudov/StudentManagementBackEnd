using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.SubjectHourDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.Attendance
{
    public class GetAttendanceForAttendanceForTeacherPageDTO
    {
        public Guid Id { get; set; }
        public bool? IsPresent { get; set; }
        public Guid SubjectHourId { get; set; }
        public DateTime Date { get; set; }
        public GetStudentForAttendanceForTeacherPageDTO Student { get; set; }
    }
}
