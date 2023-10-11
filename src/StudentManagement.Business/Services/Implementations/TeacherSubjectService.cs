using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Business.DTOs.TeacherSubjectDTOs;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.Core.Entities;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Implementations
{
    public class TeacherSubjectService : ITeacherSubjectService
    {
        private readonly ITeacherSubjectRepository _teacherSubjectRepository;
        public TeacherSubjectService(ITeacherSubjectRepository teacherSubjectRepository)
        {
            _teacherSubjectRepository = teacherSubjectRepository;
        }

        public async Task<List<TeacherSubject>> GetAllTeacherSubjectsAsync()
        {
          var teacherSubjects = await   _teacherSubjectRepository.GetAll().ToListAsync();
          return teacherSubjects;
        }
        public async Task<List<TeacherSubject>> GetTeacherSubjectsForGroupSubjectAsync(Guid groupSubjectId)
        {
            var teacherSubjects = await _teacherSubjectRepository.GetFiltered(ts=>ts.GroupSubjectId == groupSubjectId).ToListAsync();
            return teacherSubjects;
        }

        public async Task<TeacherSubject> GetGroupByIdAsync(Guid id)
        {
            var teacherSubject = await _teacherSubjectRepository.GetSingleAsync(ts=>ts.Id == id);
            return teacherSubject;
        }

        public async Task DeleteTeacherSubjectsAsync(List<TeacherSubject> teacherSubjects)
        {
            _teacherSubjectRepository.DeleteList(teacherSubjects);
           await _teacherSubjectRepository.SaveChangesAsync();
        }
    }
}
