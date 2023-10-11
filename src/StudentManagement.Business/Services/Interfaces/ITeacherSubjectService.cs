using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Repositories.Implementations;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface ITeacherSubjectService 
    {
        Task<List<TeacherSubject>> GetAllTeacherSubjectsAsync();
        Task<List<TeacherSubject>> GetTeacherSubjectsForGroupSubjectAsync(Guid groupSubjectId);
        Task DeleteTeacherSubjectsAsync(List<TeacherSubject> teacherSubjects);
        Task<TeacherSubject> GetGroupByIdAsync(Guid id);
       
    }
}
