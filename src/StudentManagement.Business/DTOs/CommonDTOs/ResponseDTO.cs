using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.DTOs.CommonDTOs
{
    public record ResponseDTO(HttpStatusCode httpStatusCode,string Message);
    

    
}
