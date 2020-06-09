using System;

namespace SentimentAnalyzer.Utils
{
    public static class ExceptionExtensions
    {
        public static string ToStringRecursively(this Exception exception)
        {
            return exception == null
                ? string.Empty
                : $"{exception}{exception.InnerException?.ToStringRecursively() ?? string.Empty}";
        }
    }
}