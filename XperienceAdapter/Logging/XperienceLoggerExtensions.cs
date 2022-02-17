using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace XperienceAdapter.Logging
{
    /// <summary>
    /// the class static so that it can hold extension methods.
    /// </summary>
    public static class XperienceLoggerExtensions
    {
        /// <summary>
        /// AddXperience method that later call at the application start.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddXperience(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, XperienceLoggerProvider>());

            return builder;
        }

        /// <summary>
        ///  a convenience LogEvent method that creates an event name from the name of the client method.
        ///  If the methodName argument is not available, the log entry will simply contain less information.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="logLevel"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogEvent(this ILogger logger, LogLevel logLevel, string? methodName, string? message = default, Exception? exception = default, params object[] args)
        {
            if (!string.IsNullOrEmpty(methodName))
            {
                var eventId = new EventId(0, methodName);

                logger.Log(logLevel, eventId, exception, message, args);
            }
            else
            {
                logger.Log(logLevel, exception, message, args);
            }
        }

    }
}
