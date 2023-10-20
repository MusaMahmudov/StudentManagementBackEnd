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
    public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.Property(f => f.Name).IsRequired(true).HasMaxLength(256);
            builder.HasMany(f => f.Groups).WithOne(g => g.Faculty);

            builder.HasCheckConstraint("NameFaculty", "Len(Name) >=3");

        }
    }
}
