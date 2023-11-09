using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.ExamDTOs
{
    public class GetExamsForExamResultUpdateDTO
    {
        public Guid Id { get; set; }
        public string examTypeName { get; set; }
        public string subjectName { get; set; }
        public string groupName { get; set; }
        public int maxScore { get; set; }
    }
}
