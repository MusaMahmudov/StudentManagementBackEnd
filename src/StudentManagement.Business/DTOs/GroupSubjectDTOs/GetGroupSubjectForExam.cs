using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class GetGroupSubjectForExam
    {
        public Guid Id { get; set; }
        public string groupName { get; set; }
        public string subjectName { get; set; }
        public byte Credits { get; set; }
    }
}
