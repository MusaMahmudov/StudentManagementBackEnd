using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.AuthExceptions
{
    public class ChangePasswordException : Exception, IBaseException
    {
        
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public string errorMessage {  get; set; }
        public ChangePasswordException( IEnumerable<IdentityError> errors) 
        {
            errorMessage = string.Join(" ",errors.Select(e=>e.Description));
        }
    }
}
