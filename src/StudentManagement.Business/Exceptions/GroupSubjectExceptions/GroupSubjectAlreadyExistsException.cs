using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.GroupSubjectExceptions
{
    public class GroupSubjectAlreadyExistsException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public string errorMessage { get; set; }
        public GroupSubjectAlreadyExistsException(string Message) : base(Message) 
        {
            errorMessage = Message;
        }
    }
}
