using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.ExamDTOs
{
    public class GetExamForExamsForTeacherPageAssign
    {
        public Guid Id { get; set; }
        public byte maxScore { get; set; }
    }
}
