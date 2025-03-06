using A.Common.Exceptions;
using A.Common.GlobalResponses;
using A.Common.GlobalResponses.Generics;
using System.Net;
using System.Text.Json;

namespace LibrarySystemProject.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            switch (error)
            {
                case BadRequestException:
                    var message = new List<string>() { error.Message };
                    await WriteError(context, HttpStatusCode.BadRequest, message);
                    break;
                case TooManyRequestException:
                    message = [error.Message];
                    await WriteError(context,HttpStatusCode.TooManyRequests, message);
                    break;
                default:
                    message = [error.Message];
                    await WriteError(context,HttpStatusCode.InternalServerError,message);
                    break;

            }
        }
    }
    public static async Task WriteError(HttpContext context,HttpStatusCode httpStatusCode,List<string> message)
    {
        context.Response.Clear();   
        context.Response.StatusCode = (int)httpStatusCode;
        context.Response.ContentType = "application/json";
        var json = JsonSerializer.Serialize(new Result(message));
        await context.Response.WriteAsync(json);
    }
}
 