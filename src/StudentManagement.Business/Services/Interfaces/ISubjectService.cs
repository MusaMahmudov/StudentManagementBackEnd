using StudentManagement.Business.DTOs.ExamTypeDTOs;
using StudentManagement.Business.DTOs.SubjectDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<GetSubjectDTO>> GetAllSubjectAsync(string? search);
        Task<GetSubjectDTO> GetSubjectByIdAsync(Guid id);
        Task CreateSubjectAsync(PostSubjectDTO postSubjectDTO);
        Task DeleteSubjectAsync(Guid id);
        Task UpdateSubjectAsync(Guid id,PutSubjectDTO putSubjectDTO);
    }
}
