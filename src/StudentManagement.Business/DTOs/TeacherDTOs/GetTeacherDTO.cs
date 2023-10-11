using StudentManagement.Business.DTOs.TeacherSubjectDTOs;
using StudentManagement.Business.DTOs.UserDTOs;
using StudentManagement.Core.Entities;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.TeacherDTOs
{
    public class GetTeacherDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public GetUserDTO? AppUser { get; set; }
        public string MobileNumber { get; set; }
        public string EMail { get; set; }
        public string Gender { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public List<GetTeacherSubjectForTeacherDTO>? teacherSubjects { get; set; }
    }
}
