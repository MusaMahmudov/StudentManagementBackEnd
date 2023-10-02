using StudentManagement.Business.DTOs.TeacherDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<List<GetTeacherDTO>> GetAllTeachersAsync(string? search);
        Task<GetTeacherDTO> GetTeacherByIdAsync(Guid id);
        Task CreateTeacherAsync(PostTeacherDTO postTeacherDTO);
        Task DeleteTeacherAsync(Guid id);
        Task UpdateTeacherAsync(Guid id,PutTeacherDTO putTeacherDTO);
    }
}
