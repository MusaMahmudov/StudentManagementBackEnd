using StudentManagement.Business.DTOs.ExamDTOs;
using StudentManagement.Business.DTOs.FacultyDTOs;
using StudentManagement.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IExamService
    {
        Task<List<GetExamDTO>> GetAllExamsAsync(string? search);
        Task<GetExamDTO> GetExamByIdAsync(Guid id);
        Task<GetExamForUpdateDTO> GetExamByIdForUpdateAsync(Guid id);
        Task<List<GetExamForSubjectsForStudentPageDTO>> GetExamsForSubjectsForStudentPageAsync(Guid groupSubjectId);
        Task<List<GetExamForExamsScheduleForUserPage>> GetExamsForExamScheduleForStudentPageAsync(Guid studentId);
        Task<List<GetExamForExamsScheduleForUserPage>> GetExamsForExamScheduleForTeacherPageAsync(Guid teacherId);

        Task<List<GetExamsForExamResultUpdateDTO>> GetAllExamsForExamResultUpdateAsync();


        Task<GetExamForExamsForTeacherPageAssign> GetExamForExamsForTeacherPageAssignAsync(Guid id);

        Task CreateExamAsync(PostExamDTO postExamDTO);
        Task DeleteExamAsync(Guid id);
        Task UpdateExamAsync(Guid id, PutExamDTO putExamDTO);
    }
}
