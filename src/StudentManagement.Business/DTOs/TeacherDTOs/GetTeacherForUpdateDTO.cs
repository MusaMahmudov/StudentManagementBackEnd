using StudentManagement.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.TeacherDTOs
{
    public class GetTeacherForUpdateDTO
    {
        public string FullName { get; set; }
        public string AppUserId { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
