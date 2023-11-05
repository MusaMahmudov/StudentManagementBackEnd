using StudentManagement.Core.Entities.Identity;
using StudentManagement.Core.Entities;
using StudentManagement.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Business.DTOs.ExamResultDTOs;

namespace StudentManagement.Business.DTOs.StudentDTOs
{
    public class GetStudentForExamsForTeacherPageDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
      
        public List<GetExamResultForExamsForTeacherPageAssign>? examResults { get; set; }
    }
}
