using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.TeacherDTOs
{
    public class GetTeacherForCreateGroupSubjectDTO
    {
        public Guid Id { get; set; }
        public string fullName { get; set; }
    }
}
