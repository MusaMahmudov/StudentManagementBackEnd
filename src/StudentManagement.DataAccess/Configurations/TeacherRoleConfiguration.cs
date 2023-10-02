using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DataAccess.Configurations
{
    public class TeacherRoleConfiguration : IEntityTypeConfiguration<TeacherRole>
    {
        public void Configure(EntityTypeBuilder<TeacherRole> builder)
        {
            builder.Property(tr=>tr.Name).IsRequired(true).HasMaxLength(128);
        }
    }
}
