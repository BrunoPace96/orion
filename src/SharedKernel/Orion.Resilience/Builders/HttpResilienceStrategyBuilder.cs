using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace Orion.Resilience.Builders
{
    public static class HttpResilienceStrategyBuilder
    {
        public static AsyncPolicyWrap<HttpResponseMessage> Build(
            TimeSpan[] sleepDurations = null,
            int handledEventsAllowedBeforeBreaking = 7,
            TimeSpan durationOfBreak = default,
            TimeSpan timeout = default
        ) => Policy.WrapAsync(
                Retry(sleepDurations),
                CircuitBreaker(handledEventsAllowedBeforeBreaking, durationOfBreak),
                Timeout(timeout)
            );

        private static AsyncRetryPolicy<HttpResponseMessage> Retry(TimeSpan[] sleepDurations = null)
        {
            IEnumerable<TimeSpan> GetSleepDurations()
            {
                if (sleepDurations == null || !sleepDurations.Any())
                {
                    return new[]
                    {
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(5)
                    };
                }

                return sleepDurations;
            }

            return HttpPolicyBuilder
                .Build()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(GetSleepDurations(), (_, timeSpan, attempts, _) =>
                {
                    Console.WriteLine(timeSpan);
                    Console.WriteLine($"{attempts}° attempt!");
                });
        }

        private static AsyncCircuitBreakerPolicy<HttpResponseMessage> CircuitBreaker(
            int handledEventsBeforeBreaking = 6,
            TimeSpan durationOfBreak = default
        ) => HttpPolicyBuilder
            .Build()
            .Or<TimeoutRejectedException>()
            .CircuitBreakerAsync(
                handledEventsBeforeBreaking,
                durationOfBreak == default ? TimeSpan.FromSeconds(60) : durationOfBreak
            );

        private static AsyncTimeoutPolicy<HttpResponseMessage> Timeout(TimeSpan timeout = default) =>
            Policy.TimeoutAsync<HttpResponseMessage>(
                timeout == default ? TimeSpan.FromSeconds(30) : timeout
            );
    }
}