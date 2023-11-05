using StudentManagement.Business.DTOs.ExamDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.ExamResultDTOs
{
    public class GetExamResultForExamsForTeacherPageAssign
    {
        public Guid Id { get; set; }
        public byte? Score { get; set; }
        public Guid examId { get; set; }
    }
}
