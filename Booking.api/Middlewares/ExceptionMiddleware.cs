﻿using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Booking.api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error",
                Details = exception.Message //test
            });

            return context.Response.WriteAsync(result);
        }
    }
}
