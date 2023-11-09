using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupSubjectDTOs
{
    public class GetGroupSubjectForExamForExamsSchedule
    {
        public string GroupName { get; set; }
        public string SubjectName { get; set; }    
   
    }
}
