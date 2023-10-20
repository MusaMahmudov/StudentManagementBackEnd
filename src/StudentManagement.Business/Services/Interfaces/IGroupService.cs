using AutoMapper;
using StudentManagement.Business.DTOs.GroupDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IGroupService 
    {
        Task<List<GetGroupDTO>> GetAllGroupsAsync(string? search);
        Task<GetGroupDTO> GetGroupByIdAsync(Guid id);
        Task<GetGroupForUpdateDTO> GetGroupByIdForUpdateAsync(Guid id);

        Task CreateGroupAsync(PostGroupDTO postGroupDTO);
        Task DeleteGroupAsync(Guid id); 
        Task UpdateGroupAsync(Guid Id,PostGroupDTO postGroupDTO);

    }
}
