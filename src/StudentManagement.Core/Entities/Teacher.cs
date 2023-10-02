using StudentManagement.Core.Entities.Common;
using StudentManagement.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Core.ValidationAttributes;
namespace StudentManagement.Core.Entities
{
    public class Teacher : BaseSectionEntity
    {
        public string FullName { get; set; }
        public AppUser? AppUser { get; set; }
        public string? AppUserId { get; set; }
        public string MobileNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.DateTime)]
        [MinimumAge(18)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public List<TeacherSubject>? teacherSubjects { get; set; }

    }
}
