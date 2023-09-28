using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.LoginDTOs
{
    public class LoginDTO
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
       
    }
}
