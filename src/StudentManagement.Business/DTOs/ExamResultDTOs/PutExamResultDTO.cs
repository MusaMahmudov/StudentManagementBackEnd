using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.ExamResultDTOs
{
    public class PutExamResultDTO
    {
        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }
        public byte? Score { get; set; }
    }
}
