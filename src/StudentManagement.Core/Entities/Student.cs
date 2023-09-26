using StudentManagement.Core.Entities.Common;
using System;
using System.Collections.Generic;
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
    public int HomePhoneNumber { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<StudentGroup>? studentGroups { get; set; }

}
