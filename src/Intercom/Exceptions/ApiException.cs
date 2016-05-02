using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using RestSharp;

namespace Library.Exceptions
{
    public class ApiException : IntercomException
    {
        public int StatusCode { set; get; }
        public String StatusDescription { set; get; }
        public String ApiResponseBody { set; get; }
        public Errors ApiErrors { set; get; }

        public ApiException ()
            :base()
        {
        }

        public ApiException (String message, Exception innerException) 
            :base(message, innerException)
        {
        }

        public ApiException (int statusCode, String statusDescription, Errors apiErrors, String apiResponseBody)
            :base()
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
            this.ApiErrors = apiErrors;
            this.ApiResponseBody = apiResponseBody;
        }

        public ApiException (String message, Exception innerException, int statusCode, String statusDescription, Errors apiErrors, String apiResponseBody)
            :base(message, innerException)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
            this.ApiErrors = apiErrors;
            this.ApiResponseBody = apiResponseBody;
        }

    }
}