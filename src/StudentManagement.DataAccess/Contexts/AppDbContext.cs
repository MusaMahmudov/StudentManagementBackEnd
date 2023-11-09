using Microsoft.AspNetCore.Http;
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
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AppDbContext(IHttpContextAccessor httpContextAccessor,DbContextOptions<AppDbContext> options) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Faculty> Faculties { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<ExamType> ExamTypes { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;


    public DbSet<TeacherRole> TeacherRoles { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<TeacherSubject> TeacherSubjects { get; set; } = null!;
    public DbSet<GroupSubject> GroupSubjects { get; set; } = null!;

    public DbSet<StudentGroup> StudentGroups { get; set; } = null!;
    public DbSet<Exam> Exams { get; set; } = null!;
    public DbSet<ExamResult> ExamsResults { get; set; } = null!;

    public DbSet<SubjectHour> SubjectHours { get; set; } = null!;

    public DbSet<Attendance> Attendances { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentConfiguration).Assembly);
        modelBuilder.Entity<Subject>().HasQueryFilter(s=>s.IsDeleted == false);
        modelBuilder.Entity<Teacher>().HasQueryFilter(t => t.IsDeleted == false);
        modelBuilder.Entity<Group>().HasQueryFilter(g => g.IsDeleted == false);
        modelBuilder.Entity<Student>().HasQueryFilter(s => s.IsDeleted == false);
        modelBuilder.Entity<ExamType>().HasQueryFilter(et => et.IsDeleted == false);
        modelBuilder.Entity<Faculty>().HasQueryFilter(f => f.IsDeleted == false);
        modelBuilder.Entity<GroupSubject>().HasQueryFilter(gs => gs.IsDeleted == false);
        modelBuilder.Entity<StudentGroup>().HasQueryFilter(sg => sg.IsDeleted == false);
        modelBuilder.Entity<TeacherRole>().HasQueryFilter(tr=>tr.IsDeleted == false);
        modelBuilder.Entity<TeacherSubject>().HasQueryFilter(ts => ts.IsDeleted == false);
        modelBuilder.Entity<Exam>().HasQueryFilter(e=>e.IsDeleted == false);
        modelBuilder.Entity<ExamResult>().HasQueryFilter(er => er.IsDeleted == false);



        modelBuilder.Entity<AppUser>().HasOne(u=>u.Student).WithOne(s => s.AppUser).HasForeignKey<Student>(s=>s.AppUserId);
        modelBuilder.Entity<AppUser>().HasOne(u => u.Teacher).WithOne(s => s.AppUser).HasForeignKey<Teacher>(s => s.AppUserId);


        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        string userName = "Admin";
        var identity = _httpContextAccessor?.HttpContext?.User.Identity;
        if(identity is not null)
        {
            userName = identity.IsAuthenticated ? identity.Name : "Admin";
        }


        var entries = ChangeTracker.Entries<BaseSectionEntity>();

        foreach (var entry in entries)
        {
            switch(entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;

                    entry.Entity.CreatedBy = userName;

                    entry.Entity.UpdatedBy = userName;

                    break;
                   case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;


                    entry.Entity.UpdatedBy = userName;

                    break;
                    

            }


        }


        return base.SaveChangesAsync(cancellationToken);
    }
}
