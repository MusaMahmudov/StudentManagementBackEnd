using StudentManagement.Business.DTOs.ExamTypeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IExamTypeService 
    {
        Task<List<GetExamTypeDTO>> GetAllExamTypesAsync(string? search);
        Task<GetExamTypeDTO> GetExamTypeByIdAsync(Guid id);
        Task<GetExamTypeForUpdateDTO> GetExamTypeByIdForUpdateAsync(Guid id);

        Task CreateExamTypeAsync(PostExamTypeDTO postExamTypeDTO);
        Task DeleteExamTypeAsync(Guid id);
        Task UpdateExamTypeAsync(Guid id, PutExamTypeDTO putExamTypeDTO);

    }
}
