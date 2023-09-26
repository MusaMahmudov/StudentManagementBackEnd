using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions
{
    public interface IBaseException
    {
        HttpStatusCode HttpStatusCode { get;  }
        string errorMessage { get;  }

    }
}
