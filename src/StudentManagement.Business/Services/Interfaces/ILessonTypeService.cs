using StudentManagement.Business.DTOs.LessonTypeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface ILessonTypeService
    {
        Task<List<GetLessonTypeDTO>> GetAllLessonTypesAsync(string? search);
        Task<GetLessonTypeDTO> GetLessonTypeByIdAsync(Guid Id);
        Task<GetLessonTypeForUpdateDTO> GetLessonTypeByIdForUpdateAsync(Guid Id);

        Task DeleteLessonTypeAsync(Guid Id);
        Task UpdateLessonTypeAsync(Guid Id,PutLessonTypeDTO putLessonTypeDTO);
        Task CreateLessonTypeAsync(PostLessonTypeDTO postLessonTypeDTO);
    }
}
