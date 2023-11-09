using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.AuthExceptions
{
    public class OldPasswordIsNotCorrectException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public string errorMessage {  get; set; }
        public OldPasswordIsNotCorrectException(string message) : base(message) 
        {
            errorMessage = message;
        }
    }
}
