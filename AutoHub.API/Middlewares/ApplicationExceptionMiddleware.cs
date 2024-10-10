using AutoHub.API.Models;
using AutoHub.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AutoHub.API.Middlewares;

public class ApplicationExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (NotFoundException nfEx)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, nfEx);
        }
        catch (LoginFailedException lfEx)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.Unauthorized, lfEx);
        }
        catch (RegistrationFailedException efEx)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.Conflict, efEx);
        }
        catch (ApplicationException aEx)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, aEx);
        }
        catch (DuplicateException dEx)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.Conflict, dEx);
        }
        catch (InvalidValueException ivEx)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ivEx);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode httpStatusCode, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;

        return context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = (int)httpStatusCode,
            Instance = context.Request.Path,
            Type = ex.GetType().ToString(),
            Message = ex.Message,
            Details = ex.GetBaseException().Message,
            StackTrace = ex.StackTrace,
        }.ToString());
    }
}
