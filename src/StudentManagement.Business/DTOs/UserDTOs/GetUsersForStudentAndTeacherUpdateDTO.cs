using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.UserDTOs
{
    public class GetUsersForStudentAndTeacherUpdateDTO
    {
        public Guid Id { get; set; }
        public string userName { get; set; }
    }
}
