using StudentManagement.Core.Entities.Identity;
using StudentManagement.Core.Entities;
using StudentManagement.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Business.DTOs.Attendance;

namespace StudentManagement.Business.DTOs.StudentDTOs
{
    public class GetStudentForAttendanceForTeacherPageDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        //public List<GetAttendanceForStudentAttendancePageDTO>? Attendances { get; set; }
     
    
    }
}
