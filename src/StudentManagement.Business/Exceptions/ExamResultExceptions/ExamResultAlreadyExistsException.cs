using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.ExamResultExceptions
{
    public class ExamResultAlreadyExistsException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode =>HttpStatusCode.BadRequest;

        public string errorMessage {get;set; }
        public ExamResultAlreadyExistsException(string message) :base(message) 
        {
            errorMessage = message;
        }
    }
}
