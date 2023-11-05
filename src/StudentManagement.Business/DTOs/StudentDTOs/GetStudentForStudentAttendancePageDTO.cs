using StudentManagement.Business.DTOs.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.StudentDTOs
{
    public class GetStudentForStudentAttendancePageDTO
    {
        public List<GetAttendanceForStudentAttendancePageDTO>? Attendances { get; set; }
    }
}
