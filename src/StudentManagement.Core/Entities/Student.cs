using StudentManagement.Core.Entities.Common;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.Entities;

public class Student : BaseSectionEntity
{
    public string FullName { get; set; }
    public int YearOfGraduation { get; set; }
    public string Gender { get; set; }
    public string EducationDegree { get; set; }
    public string FormOfEducation {get;set; }
    public string TypeOfPayment { get; set; }
    public string HomePhoneNumber { get; set; }
    public string PhoneNumber { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public AppUser? AppUser { get; set; }
    public string? AppUserId { get; set; }
    [DataType(DataType.DateTime)]
    [MinimumAge(18)]
    public DateTime DateOfBirth { get; set; }
    public Group? Group { get; set; }
    public Guid? GroupId { get; set; }
    public List<StudentGroup>? studentGroups { get; set; }
    public List<ExamResult>? examResults { get; set; }

}
