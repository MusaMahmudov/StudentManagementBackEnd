using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.AuthExceptions
{
    public class PasswordsAreSameException : Exception,IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public string errorMessage { get; set; }
        public PasswordsAreSameException(string Message) : base(Message)
        {
            errorMessage = Message;
        }

    }
}
