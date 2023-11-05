using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.ExamDTOs
{
    public class GetExamForExamResultForStudentPageDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ExamType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public byte maxScore { get; set; }
    }
}
