using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Threading.Tasks;
using SentimentAnalyzer.Utils;

namespace SentimentAnalyser.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _logger = logger;
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
                _logger.Error(ex.ToStringRecursively());
                throw;
            }
        }
    }
}
