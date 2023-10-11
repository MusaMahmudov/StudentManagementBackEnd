using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.TeacherRoleExceptions
{
    public class TeacherRoleNotFoundByIdException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

        public string errorMessage { get; }
        public TeacherRoleNotFoundByIdException(string Message) : base(Message) 
        {
            errorMessage = Message;
        }
    }
}
