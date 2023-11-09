using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class GetGroupSubjectForExamUpdateDTO
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string SubjectName { get; set; }
    }
}
