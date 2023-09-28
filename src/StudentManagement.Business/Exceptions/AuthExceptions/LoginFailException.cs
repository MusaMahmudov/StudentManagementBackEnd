using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.AuthExceptions
{
    public class LoginFailException : Exception, IBaseException
    {
        public LoginFailException(string Message) : base(Message) 
        {
            errorMessage = Message;

        }
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public string errorMessage { get; }
    }
}
