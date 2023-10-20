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
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {

            builder.Property(t=>t.FullName).IsRequired(true).HasMaxLength(128);
            builder.Property(t=>t.MobileNumber).IsRequired(true).HasMaxLength(128);
            builder.Property(t=>t.EMail).IsRequired(true).HasMaxLength(128);
            builder.Property(t=>t.Gender).IsRequired(true).HasMaxLength(128);
            builder.Property(t => t.DateOfBirth).IsRequired(true);
            builder.Property(t=>t.Address).IsRequired(true).HasMaxLength(128);

            builder.HasCheckConstraint("FullNameTeacher", "Len(FullName) >=3");

        }
    }
}
