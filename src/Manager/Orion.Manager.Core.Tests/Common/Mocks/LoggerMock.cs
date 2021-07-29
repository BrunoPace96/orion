using System;
using Microsoft.Extensions.Logging;

namespace Orion.Manager.Core.Tests.Common.Mocks
{
    public class LoggerMock<TValue>: ILogger<TValue>
    {
        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter
        ) => Console.WriteLine($"{formatter(state, exception)}");
    }
}