using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.StudentDTOs
{
    public class GetStudentForUser
    {
        public Guid Id { get; set; }
        public string studentName { get; set; }
    }
}
