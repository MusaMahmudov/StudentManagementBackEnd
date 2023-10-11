using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.GroupExceptions
{
    public class GroupNotFoundByIdException : Exception, IBaseException
    {
        public GroupNotFoundByIdException(string Message)  : base(Message)
        {
            errorMessage = Message;
        }
        public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

        public string errorMessage { get; }
    }
}
