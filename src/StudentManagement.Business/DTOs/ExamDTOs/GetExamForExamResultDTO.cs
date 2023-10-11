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
    public class GetExamForExamResultDTO
    {
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public string ExamTypeName { get; set; }
        public GetGroupSubjectForExamResult GroupSubject { get; set; }
    }
}
