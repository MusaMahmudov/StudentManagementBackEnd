using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.StudentExceptions
{
    public class StudentNotFoundByIdException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;
        public string errorMessage { get; }
        public StudentNotFoundByIdException(string message) : base(message) 
        { 
            errorMessage = message;
        
        }
       
    }
}
