using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.RoleExceptions
{
    public class RoleNotFoundByIdException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

        public string errorMessage {get; set;}
        public RoleNotFoundByIdException(string Message) : base(Message)
        {
            errorMessage = Message;
        }
    }
}
