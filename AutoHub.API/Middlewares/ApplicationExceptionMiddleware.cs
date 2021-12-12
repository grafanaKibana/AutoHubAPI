using AutoHub.API.Models;
using AutoHub.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AutoHub.API.Middlewares
{
    public class ApplicationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ApplicationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException nfEx)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, nfEx.Message);
            }
            catch (LoginFailedException lfEx)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.Unauthorized, lfEx.Message);
            }
            catch (RegistrationFailedException efEx)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.Conflict, efEx.Message);
            }
            catch (ApplicationException aEx)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, aEx.Message);
            }
            catch (DublicateException dEx)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.Conflict, dEx.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode httpStatusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = (int)httpStatusCode,
                Message = message,
                Instance = context.Request.Path
            }.ToString());
        }
    }
}