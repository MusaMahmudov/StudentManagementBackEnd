using StudentManagement.Business.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task LogOutAsync();
    }
}
