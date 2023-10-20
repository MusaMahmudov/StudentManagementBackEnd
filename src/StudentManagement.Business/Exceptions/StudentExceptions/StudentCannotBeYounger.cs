using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.StudentExceptions
{
    public class StudentCannotBeYounger : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode =>HttpStatusCode.BadRequest;

        public string errorMessage { get; set; }
        public StudentCannotBeYounger(string Message) : base(Message) 
        {
            errorMessage = Message;
        }
    }
}
