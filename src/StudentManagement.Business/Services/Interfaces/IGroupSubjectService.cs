using StudentManagement.Business.DTOs.GroupSubjectDTOs;
using StudentManagement.Business.DTOs.TeacherDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IGroupSubjectService
    {
        Task<List<GetGroupSubjectDTO>> GetAllGroupSubjectsAsync();
        Task<GetGroupSubjectDTO> GetGroupSubjectByIdAsync(Guid id);
        List<GetGroupSubjectForTeacherPageDTO> GetGroupSubjectForTeacherPageDTO(Guid teacherId);
        Task<List<GetGroupSubjectForSubjectsForStudentPageDTO>> GetGroupSubjectForSubjectsForStudentPageAsync(Guid studentId);
        Task<List<GetGroupSubjectForExamUpdateDTO>> GetGroupSubjectsForExamUpdateAsync();
        Task<GetGroupSubjectForUpdateDTO> GetGroupSubjectForUpdateAsync(Guid id);
        Task CreateGroupSubjectAsync(PostGroupSubjectDTO postGroupSubjectDTO);
        Task DeleteGroupSubjectAsync(Guid id);
        Task UpdateGroupSubjectAsync(Guid id, PutGroupSubjectDTO putGroupSubjectDTO);
    }
}
