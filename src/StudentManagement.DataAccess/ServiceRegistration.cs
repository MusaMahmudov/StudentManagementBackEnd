﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.DataAccess.Contexts;
using StudentManagement.DataAccess.Repositories.Implementations;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DataAccess
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection service,IConfiguration configuration)
        {

           service.AddScoped<IStudentRepository, StudentRepository>();
            service.AddScoped<IFacultyRepository, FacultyRepository>();
            service.AddScoped<IGroupRepository, GroupRepository>();
            service.AddScoped<IExamTypeRepository, ExamTypeRepository>();
            service.AddScoped<ITeacherRepository, TeacherRepository>();
            service.AddScoped<ISubjectRepository, SubjectRepository>();
            service.AddScoped<ITeacherRoleRepository, TeacherRoleRepository>();
            service.AddScoped<IGroupSubjectRepository, GroupSubjectRepository>();
            service.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();
            service.AddScoped<ILessonTypeRepository, LessonTypeRepository>();
            service.AddScoped<IExamRepository, ExamRepository>();
            service.AddScoped<IExamResultRepository, ExamResultRepository>();
            service.AddScoped<ISubjectHourRepository, SubjectHourRepository>();
            service.AddScoped<IStudentGroupRepository, StudentGroupRepository>();
            service.AddScoped<IAttendanceRepository, AttendanceRepository>();







            service.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            return service;
        }

    }
}
