using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.UserDTOs
{
    public class GetUserForUpdateDTO
    {
        public string UserName { get; set; }

        public Guid? StudentId { get; set; }
        public Guid? TeacherId { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public List<string>? RoleId { get; set; }
    }
}
