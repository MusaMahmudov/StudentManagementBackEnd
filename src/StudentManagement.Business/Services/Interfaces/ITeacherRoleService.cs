using StudentManagement.Business.DTOs.ExamTypeDTOs;
using StudentManagement.Business.DTOs.TeacherRoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface ITeacherRoleService
    {
        Task<List<GetTeacherRoleDTO>> GetAllTeacherRolesAsync(string? search);
        Task<GetTeacherRoleDTO> GetTeacherRoleByIdAsync(Guid id);
        Task CreateTeacherRoleAsync(PostTeacherRoleDTO postTeacherRoleDTO);
        Task DeleteTeacherRoleAsync(Guid id);
        Task UpdateTeacherRoleAsync(Guid id, PutTeacherRoleDTO putTeacherRoleDTO);
    }
}
