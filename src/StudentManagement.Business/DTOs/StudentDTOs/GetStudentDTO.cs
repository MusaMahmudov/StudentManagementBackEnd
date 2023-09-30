using StudentManagement.Business.DTOs.GroupDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.StudentDTOs
{
    public class GetStudentDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int YearOfGraduation { get; set; }
        public string Gender { get; set; }
        public string EducationDegree { get; set; }
        public string FormOfEducation { get; set; }
        public string TypeOfPayment { get; set; }
        public string HomePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<GetGroupStudentDTO>? Groups { get; set; }
    }
}
