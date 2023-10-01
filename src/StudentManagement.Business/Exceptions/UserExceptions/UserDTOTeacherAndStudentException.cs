using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.UserExceptions
{
    public class UserDTOTeacherAndStudentException : Exception,IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public string errorMessage { get; }
        public UserDTOTeacherAndStudentException(string Message) : base(Message)
        {
            errorMessage = Message;
        }


    }
}
