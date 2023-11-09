using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DataAccess.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.FullName).IsRequired(true).HasMaxLength(128);
           builder.Property(s=>s.IsDeleted).HasDefaultValue(false);
            builder.Property(s => s.YearOfGraduation).IsRequired(true);
            builder.Property(s => s.Gender).IsRequired(true);
            builder.Property(s => s.EducationDegree).IsRequired(true).HasMaxLength(128);
            builder.Property(s => s.FormOfEducation).IsRequired(true).HasMaxLength(128);
            builder.Property(s => s.TypeOfPayment).IsRequired(true).HasMaxLength(128);
            builder.Property(s => s.HomePhoneNumber).IsRequired(true);
            builder.Property(s => s.PhoneNumber).IsRequired(true);
            builder.Property(s => s.Email).IsRequired(true);


            builder.HasCheckConstraint("FormOfEducationStudent", "Len(FormOfEducation) >=3");
            builder.HasCheckConstraint("EducationDegreeStudent", "Len(EducationDegree) >=3");
            builder.HasCheckConstraint("TypeOfPaymentStudent", "Len(TypeOfPayment) >=3");
            builder.HasCheckConstraint("HomePhoneNumberStudent", "Len(HomePhoneNumber) >=3");
            builder.HasCheckConstraint("PhoneNumberStudent", "Len(PhoneNumber) >=3");
            builder.HasCheckConstraint("EmailStudent", "Len(Email) >=3");
            builder.HasCheckConstraint("FullNameStudent", "Len(FullName) >=3");
            builder.HasCheckConstraint("YearOfGraduation", $"YearOfGraduation Between 1900 and {DateTime.Now.Year}");



        }
    }
}
