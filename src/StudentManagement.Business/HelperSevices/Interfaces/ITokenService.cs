using StudentManagement.Business.DTOs.AuthDTOs;
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
        Task<TokenResponseDTO> CreateToken(AppUser User);
    }
}
