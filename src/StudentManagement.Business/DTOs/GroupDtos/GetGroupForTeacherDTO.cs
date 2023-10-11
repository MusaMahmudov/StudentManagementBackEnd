using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupDtos
{
    public class GetGroupForTeacherDTO
    {
        public string Name { get; set; }
        public byte StudentCount { get; set; }
        public byte Year { get; set; }
        public string FacultyName { get; set; }
      
    }
}
