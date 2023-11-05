using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.ExamResultDTOs
{
    public class GetExamResultForExamForTeacherPageDTO
    { 
        public Guid studentId { get; set; }
        public string studentName { get; set; }
        public byte? score { get; set; }
    }
}
