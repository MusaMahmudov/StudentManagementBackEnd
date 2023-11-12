using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IStudentService
    {
        public Task<List<GetStudentDTO>> GetAllStudentsAsync(string? search);
        public Task<GetStudentDTO> GetStudentByIdAsync(Guid Id);
        public Task<GetStudentForUpdateDTO> GetStudentByIdForUpdateAsync(Guid Id);

        public Task<GetStudentForStudentPageDTO> GetStudentForStudentPageAsync(Guid Id);
        public Task<List<GetStudentForAttendanceForTeacherPageDTO>> GetStudentForAttendanceForTeacherPageAsync(Guid groupSubjectId);
        public Task<List<GetStudentForUser>> GetAllStudentsForUserUpdateAsync();
        public Task<List<GetStudentForExamsForTeacherPageDTO>> GetStudentsForExamsForTeacherPageAsync(Guid groupSubjectId);
        public Task<List<GetStudentForGroupUpdateDTO>> GetStudentsForGoupUpdateAsync(Guid groupId);
        public Task<List<GetStudentForGroupForDetailsDTO>> GetStudentsForGroupForDetailsAsync(Guid groupId);
        public Task<List<GetStudentForCreateOrUpdateForExamResultDTO>> GetStudentsForCreateOrUpdateForExamResultAsync();
        public Task<GetStudentForStudentAttendancePageDTO> GetStudentForStudentAttendancePageDTOAsync(Guid studentId, Guid groupSubjectId);

        public Task CreateStudentAsync(PostStudentDTO postStudentDTO);
        public Task DeleteStudentAsync(Guid Id);
        public Task UpdateStudentAsync(Guid Id,PutStudentDTO putStudentDTO);
        public Task<bool> CheckStudentExistsByIdAsync(Guid Id);

    }
}
