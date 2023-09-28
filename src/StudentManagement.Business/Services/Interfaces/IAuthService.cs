using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task LoginAsync(string userName,string Password);
    }
}
