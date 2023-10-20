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
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.Property(e => e.Name).IsRequired(true).HasMaxLength(128);
            builder.Property(e => e.Date).IsRequired(true);

            builder.HasCheckConstraint("NameExam", "Len(Name) >=3");
            builder.HasCheckConstraint("MaxScore", "MaxScore Between 0 AND 100");


        }
    }
}
