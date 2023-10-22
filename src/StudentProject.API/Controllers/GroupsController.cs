using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.GroupDtos;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetAllGroups([FromQuery]string? search)
        {
           var groups = await _groupService.GetAllGroupsAsync(search);
            return Ok(groups);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateGroup(PostGroupDTO postGroupDTO)
        {
           await _groupService.CreateGroupAsync(postGroupDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Group successefully created"));
        }

        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetGroupById(Guid Id)
        {
            var group = await _groupService.GetGroupByIdAsync(Id);
            return Ok(group);

        }
        [HttpGet("update/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetGroupByIdForUpdate(Guid Id)
        {
            var group = await _groupService.GetGroupByIdForUpdateAsync(Id);
            return Ok(group);

        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteGroup(Guid Id) 
        { 
           await _groupService.DeleteGroupAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK,"Group deleted"));
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateGroup(Guid Id,PostGroupDTO postGroupDTO)
        {
           await _groupService.UpdateGroupAsync(Id, postGroupDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Group updated "));
        }

    }
}
