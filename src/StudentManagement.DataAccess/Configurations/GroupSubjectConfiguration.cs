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
    public class GroupSubjectConfiguration : IEntityTypeConfiguration<GroupSubject>
    {
        public void Configure(EntityTypeBuilder<GroupSubject> builder)
        {
            builder.Property(gs => gs.Credits).IsRequired();
            builder.Property(gs=>gs.Hours).IsRequired();
            builder.Property(gs=>gs.TotalWeeks).IsRequired();
            builder.Property(gs=>gs.Semester).IsRequired();
            builder.Property(gs => gs.Year).IsRequired();



            builder.HasCheckConstraint("Credits", "Credits BETWEEN 1 AND 30");
            builder.HasCheckConstraint("YearOfSubject", $"Year BETWEEN 2010 AND {DateTime.Now.Year}");
            builder.HasCheckConstraint("HoursOfSubject", "Hours BETWEEN 1 AND 200");
            builder.HasCheckConstraint("TotalWeeksDuration", "TotalWeeks BETWEEN 1 AND 50");




        }
    }
}
