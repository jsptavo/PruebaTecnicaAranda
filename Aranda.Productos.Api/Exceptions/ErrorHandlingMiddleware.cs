using Aranda.Productos.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Aranda.Productos.Api.Exceptions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var result = GetResult(ex);
            result.Message = ex.Message;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.Code;
            return context.Response.WriteAsync(result.ToString());
        }

        private static ErrorResponse GetResult(Exception ex)
        {
            var errorResponse = new ErrorResponse();
            switch (ex)
            {
                case BaseLogicException _:
                    errorResponse.Code = (int)((BaseLogicException)ex).Code;
                    errorResponse.CustomData = ((BaseLogicException)ex).CustomData;
                    break;
                default:
                    errorResponse.Code = 500;
                    errorResponse.CustomData = null;
                    break;
            }
            return errorResponse;
        }
    }
}
