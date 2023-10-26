using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Business.DTOs.LessonTypeDTOs;
using StudentManagement.Business.Services.Interfaces;
using System.Net;

namespace StudentProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonTypesController : ControllerBase
    {
        private readonly ILessonTypeService _lessonTypeService;
        public LessonTypesController(ILessonTypeService lessonTypeService)
        {
            _lessonTypeService = lessonTypeService;
        }
        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetAllLessonTypes(string? search) 
        {
            var lessonTypes = await _lessonTypeService.GetAllLessonTypesAsync(search);
            return Ok(lessonTypes);
        }
        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateLessonType(PostLessonTypeDTO postLessonTypeDTO)
        {
           await _lessonTypeService.CreateLessonTypeAsync(postLessonTypeDTO);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Lesson's type successefully created "));
        }
        [HttpGet("{Id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetLessonTypeById(Guid Id)
        {
          var lessonType = await _lessonTypeService.GetLessonTypeByIdAsync(Id);
            return Ok(lessonType);
        }
        [HttpGet("update/{Id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> GetLessonTypeByIdForUpdate(Guid Id)
        {
            var lessonType = await _lessonTypeService.GetLessonTypeByIdForUpdateAsync(Id);
            return Ok(lessonType);
        }
        [HttpDelete("{Id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteLessonType(Guid Id)
        {
           await _lessonTypeService.DeleteLessonTypeAsync(Id);
            return StatusCode((int)HttpStatusCode.OK,new ResponseDTO(HttpStatusCode.OK,"Lesson's Type deleted"));
        }
        [HttpPut("{Id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateLessonType(Guid Id,PutLessonTypeDTO putLessonTypeDTO)
        {
           await _lessonTypeService.UpdateLessonTypeAsync(Id,putLessonTypeDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO(HttpStatusCode.OK, "Lesson's Type updated"));

        }
    }
}
