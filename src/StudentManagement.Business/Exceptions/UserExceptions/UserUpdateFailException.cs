using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.UserExceptions
{
    public class UserUpdateFailException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;

        public string errorMessage { get; }
        public UserUpdateFailException(IEnumerable<IdentityError> errors)
        {
            errorMessage = string.Join(" ", errors.Select(e=>e.Description));
        }
    }
}
