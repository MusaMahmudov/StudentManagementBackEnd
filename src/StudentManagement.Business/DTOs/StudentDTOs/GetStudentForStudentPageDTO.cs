using StudentManagement.Business.DTOs.ExamResultDTOs;
using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.StudentDTOs
{
    public class GetStudentForStudentPageDTO
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
        public DateTime DateOfBirth { get; set; }
        public GetMainGroupForStudentPageDTO? MainGroup { get; set; }
        public GetUserDTO? AppUser { get; set; }
        public List<GetExamResultDTO>? examResults { get; set; }
        public List<GetGroupStudentDTO>? Groups { get; set; }
    }
}
