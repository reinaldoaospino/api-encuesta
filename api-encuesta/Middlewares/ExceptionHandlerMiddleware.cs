using System;
using System.Net;
using Newtonsoft.Json;
using api_encuesta.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace api_encuesta.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ApplicationException ex)
            {

                await HandleException(context.Response, ex, HttpStatusCode.BadRequest);
            }

            catch (Exception ex)
            {
                await HandleException(context.Response, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleException(HttpResponse response, Exception ex, HttpStatusCode statusCode)
        {
            var error = JsonConvert.SerializeObject(new ErrorModel(ex.Message));

            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";
            await response.WriteAsync(error);
        }
    }
}