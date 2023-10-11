using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Business.HelperSevices.Implementations;
using StudentManagement.Business.HelperSevices.Interfaces;
using StudentManagement.Business.Mappers;
using StudentManagement.Business.Services.Implementations;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.DataAccess.Repositories.Implementations;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(StudentMapper).Assembly);

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExamTypeService, ExamTypeService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITeacherRoleService, TeacherRoleService>();  
            services.AddScoped<IGroupSubjectService, GroupSubjectService>();
            services.AddScoped<ITeacherSubjectService,TeacherSubjectService>();
            services.AddScoped<ILessonTypeService, LessonTypeService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IExamResultService, ExamResultService>();
            services.AddScoped<ISubjectHourService, SubjectHourService>();
            services.AddScoped<IStudentGroupService, StudentGroupService>();    



            return services;
        }
    }
}
