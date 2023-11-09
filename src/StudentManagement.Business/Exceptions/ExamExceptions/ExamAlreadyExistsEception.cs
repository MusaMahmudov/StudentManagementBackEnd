using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.ExamExceptions
{
    public class ExamAlreadyExistsEception : Exception,IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public string errorMessage { get; }
        public ExamAlreadyExistsEception(string Message) : base(Message)
        {
            errorMessage = Message;
        }
    }
}
