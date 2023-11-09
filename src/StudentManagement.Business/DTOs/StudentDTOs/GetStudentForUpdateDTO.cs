using StudentManagement.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.StudentDTOs
{
    public class GetStudentForUpdateDTO
    {
        public string FullName { get; set; }
        public int YearOfGraduation { get; set; }
        public string Gender { get; set; }
        public string EducationDegree { get; set; }
        public string FormOfEducation { get; set; }
        public string TypeOfPayment { get; set; }
        public string HomePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    
        public DateTime DateOfBirth { get; set; }
        public string? AppUserId { get; set; }
        public Guid GroupId { get; set; }
    }
}
