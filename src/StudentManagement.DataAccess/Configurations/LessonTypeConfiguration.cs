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
    public class LessonTypeConfiguration : IEntityTypeConfiguration<LessonType>
    {
        public void Configure(EntityTypeBuilder<LessonType> builder)
        {
            builder.Property(lt => lt.Name).IsRequired(true).HasMaxLength(128);

            builder.HasCheckConstraint("NameLessonType", "Len(Name) >=3");

        }
    }
}
