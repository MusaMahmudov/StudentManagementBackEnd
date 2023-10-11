using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.ExamResultExceptions
{
    public class ExamResultNotFoundByIdException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

        public string errorMessage { get; }
        public ExamResultNotFoundByIdException(string Message) : base(Message) 
        {
            errorMessage = Message;

        }
    }
}
