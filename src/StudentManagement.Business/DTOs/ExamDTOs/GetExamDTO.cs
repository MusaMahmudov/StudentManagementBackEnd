using StudentManagement.Business.DTOs.ExamResultDTOs;
using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.ExamDTOs
{
    public class GetExamDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public string ExamType { get; set; }
        public byte maxScore { get; set; }
        public GetGroupSubjectForExam GroupSubject { get; set; }
        public List<GetExamResultForExam>? ExamResults { get; set; }
    }
}
