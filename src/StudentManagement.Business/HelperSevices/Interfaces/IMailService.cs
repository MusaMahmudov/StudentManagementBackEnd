using StudentManagement.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.HelperSevices.Interfaces
{
    public interface IMailService
    {
        Task SendEmail(MailRequestDTO mailRequestDTO);

    }
}
