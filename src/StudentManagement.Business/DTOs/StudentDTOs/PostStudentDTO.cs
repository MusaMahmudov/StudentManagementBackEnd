using StudentManagement.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.StudentDTOs
{
    public class PostStudentDTO
    {
        public string FullName { get; set; }
        public int YearOfGraduation { get; set; }
        public string Gender { get; set; }
        public string EducationDegree { get; set; }
        public string FormOfEducation { get; set; }
        public string TypeOfPayment { get; set; }
        public string HomePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.DateTime)]
        [MinimumAge(18)]
        public DateTime DateOfBirth { get; set; }
        public string? AppUserId { get; set; }
        public Guid? MainGroup { get; set; }
        //public List<Guid>? SubGroupsId { get; set; }
    }
}
