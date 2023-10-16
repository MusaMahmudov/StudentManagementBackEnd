using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.ExamDTOs
{
    public class PutExamDTO
    {
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public byte maxScore { get; set; }

        public DateTime Date { get; set; }
        public Guid? ExamTypeId { get; set; }
        public Guid? GroupSubjectId { get; set; }
    }
}
