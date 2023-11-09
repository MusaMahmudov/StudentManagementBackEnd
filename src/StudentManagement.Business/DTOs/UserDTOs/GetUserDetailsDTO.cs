using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.TeacherDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.UserDTOs
{
    public class GetUserDetailsDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public GetStudentForUser? Student { get; set; }
        public GetTeacherForUser? Teacher { get; set; }
        public List<string> Roles { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
