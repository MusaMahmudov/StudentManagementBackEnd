using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.StudentDTOs
{
    public class GetStudentForCreateOrUpdateForExamResultDTO
    {
        public Guid Id { get; set; }
        public string fullName { get; set; }
        public string groupName { get; set; }
    }
}
