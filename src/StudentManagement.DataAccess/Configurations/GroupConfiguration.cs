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
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(g => g.Name).IsRequired(true).HasMaxLength(128);
            builder.Property(g => g.Year).IsRequired(true);
            builder.HasCheckConstraint("Year","Year BETWEEN 1 AND 10");
            builder.HasOne(g => g.Faculty).WithMany(f => f.Groups);

            builder.HasCheckConstraint("NameGroup", "Len(Name) >=3");

        }
    }
}
