using StudentManagement.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.TeacherDTOs
{
    public class PutTeacherDTO
    {
        public string FullName { get; set; }
        public string? AppUserId { get; set; }
        public string MobileNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.DateTime)]
        [MinimumAge(18)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

    }
}
