using StudentManagement.Business.DTOs.ExamDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.ExamResultDTOs
{
    public class GetExamResultForUpdateDTO
    {
        public Guid studentId { get; set; }
        public Guid examId { get; set; }
        public byte? Score { get; set; }
    }
}
