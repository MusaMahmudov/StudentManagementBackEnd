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

        Task CreateExamAsync(PostExamDTO postExamDTO);
        Task DeleteExamAsync(Guid id);
        Task UpdateExamAsync(Guid id, PutExamDTO putExamDTO);
    }
}
