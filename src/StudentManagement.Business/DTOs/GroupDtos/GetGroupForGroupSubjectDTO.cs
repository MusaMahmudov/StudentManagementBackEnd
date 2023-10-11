using StudentManagement.Business.DTOs.StudentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupDtos
{
    public class GetGroupForGroupSubjectDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public byte Year { get; set; }
        public string FacultyName { get; set; }
       

    }
}
