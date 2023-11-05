using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.ExamResultDTOs
{
    public class PostExamResultDTO
    {
        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }
        public int? Score { get; set; }
    }
}
