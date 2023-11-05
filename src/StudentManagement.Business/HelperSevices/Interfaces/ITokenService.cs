using StudentManagement.Business.DTOs.AuthDTOs;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.TeacherDTOs;
using StudentManagement.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.HelperSevices.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponseDTO> CreateToken(AppUser User,StudentForTokenDTO? student,TeacherForTokenDTO? teacher);
    }
}
