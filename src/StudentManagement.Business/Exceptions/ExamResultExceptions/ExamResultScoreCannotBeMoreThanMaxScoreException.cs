﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.Exceptions.ExamResultExceptions
{
    public class ExamResultScoreCannotBeMoreThanMaxScoreException : Exception, IBaseException
    {
        public HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public string errorMessage { get; }
        public ExamResultScoreCannotBeMoreThanMaxScoreException(string Message) : base(Message) 
        {
            errorMessage = Message;
        }
    }
}
