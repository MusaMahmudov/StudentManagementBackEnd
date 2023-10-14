using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.GroupDtos
{
    public class GetMainGroupForStudentDTO
    {
        public Guid Id { get; set; }
        public string facultyName { get; set; }
        public string Name { get; set; }
    }
}
