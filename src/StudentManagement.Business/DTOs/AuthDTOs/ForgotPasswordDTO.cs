using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.AuthDTOs
{
    public class ForgotPasswordDTO
    {
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
