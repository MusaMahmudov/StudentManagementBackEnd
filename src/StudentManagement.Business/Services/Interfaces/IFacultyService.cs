using StudentManagement.Business.DTOs.FacultyDTOs;
using StudentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IFacultyService 
    {
        Task<List<GetFacultyDTO>> GetAllFacultiesAsync(string? search);
        Task<GetFacultyDTO> GetFacultyByIdAsync(Guid id);
        Task<GetFacultyForUpdateDTO> GetFacultyByIdForUpdateAsync(Guid id);

        Task CreateFacultyAsync(PostFacultyDTO postFacultyDTO);
        Task DeleteFacultyAsync(Guid id);
        Task UpdateFacultyAsync(Guid id, PutFacultyDTO putFacultyDTO);


    }
}
