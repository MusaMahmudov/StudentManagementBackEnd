using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.SubjectHourDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.Attendance
{
    public class GetAttendanceForTeacherPageDTO
    {
        public Guid Id { get; set; }
        public GetStudentForAttendanceForTeacherPageDTO Student { get; set; }
        //public GetSubjectHourForAttendanceForTeacherPageDTO SubjectHour { get; set; }
        public bool? IsPresent { get; set; }
    }
}
