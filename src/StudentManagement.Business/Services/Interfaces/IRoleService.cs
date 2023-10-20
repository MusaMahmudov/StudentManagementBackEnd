using StudentManagement.Business.DTOs.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IRoleService
    {
        List<GetRoleDTO> GetRoles();
    }
}
