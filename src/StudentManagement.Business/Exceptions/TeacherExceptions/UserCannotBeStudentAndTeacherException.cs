using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.TeacherExceptions
{
    public class UserCannotBeStudentAndTeacherException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public string errorMessage { get; }
        public UserCannotBeStudentAndTeacherException(string Message) : base(Message) 
        {
        errorMessage = Message;
        }
    }
}
