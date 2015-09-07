using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace HelixLeisure.Filters
{
    public class CustomExceptions : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {            
            Exception innerMostException = context.Exception;
            while (innerMostException.InnerException != null) innerMostException = innerMostException.InnerException;

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(context.Exception.Message),
                ReasonPhrase = innerMostException.Message.Replace("\r\n", "")
            });
        }
    }
}