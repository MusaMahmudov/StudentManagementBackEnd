using StudentManagement.Business.DTOs.ExamDTOs;
using StudentManagement.Business.DTOs.ExamResultDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IExamResultService 
    {
        Task<List<GetExamResultDTO>> GetAllExamResultsAsync(string? studentName);
        Task<GetExamResultDTO> GetExamResultByIdAsync(Guid id);

        Task<GetExamResultForExamForStudentPageDTO> GetExamResultForExamForStudentPageAsync(Guid examId,Guid studentId);
        Task<List<GetExamResultForExamForStudentPageDTO>> GetExamResultsForFinalExamForStudentPageAsync(Guid studentId);
        Task<GetExamResultForUpdateDTO> GetExamResultForUpdateAsync(Guid Id);

        Task CreateExamResultAsync(PostExamResultDTO postExamResultDTO);
        Task DeleteExamResultAsync(Guid id);
        Task UpdateExamResultAsync(Guid id, PutExamResultDTO putExamResultDTO);
    }
}
