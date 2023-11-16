using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.SubjectHourExceptions
{
    public class StartTimeCannotBeMoreThanEndTimeException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public string errorMessage {get;set; }
        public StartTimeCannotBeMoreThanEndTimeException(string message) : base(message)
        {
            errorMessage = message;
        }
    }
}
