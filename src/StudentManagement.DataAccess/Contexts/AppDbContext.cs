using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Core.Entities;
using StudentManagement.Core.Entities.Common;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.DataAccess.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DataAccess.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Faculty> Faculties { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<ExamType> ExamTypes { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentConfiguration).Assembly);

        
        modelBuilder.Entity<AppUser>().HasOne(u=>u.Student).WithOne(s => s.AppUser).HasForeignKey<Student>(s=>s.AppUserId);
        modelBuilder.Entity<AppUser>().HasOne(u => u.Teacher).WithOne(s => s.AppUser).HasForeignKey<Teacher>(s => s.AppUserId);


        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseSectionEntity>();
        foreach (var entry in entries)
        {
            switch(entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;

                    entry.Entity.CreatedBy = "Musa";

                    entry.Entity.UpdatedBy = "Musa";

                    break;
                   case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;


                    entry.Entity.UpdatedBy = "Musa";

                    break;
                    

            }


        }


        return base.SaveChangesAsync(cancellationToken);
    }
}
