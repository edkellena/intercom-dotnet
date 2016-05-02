using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using RestSharp;

namespace Library.Exceptions
{
    public class GenericException : IntercomException
    {
        public GenericException ()
            :base()
        {
        }

        public GenericException (String message, Exception innerException) 
            :base(message, innerException)
        {
        }
    }
}