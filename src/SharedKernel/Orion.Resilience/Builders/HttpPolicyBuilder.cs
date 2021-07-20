using System;
using System.Net;
using System.Net.Http;
using Polly;

namespace Orion.Resilience.Builders
{
    public static class HttpPolicyBuilder
    {
        public static PolicyBuilder<HttpResponseMessage> Build() =>
            Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrTransientHttpStatusCode();

        private static PolicyBuilder<HttpResponseMessage> OrTransientHttpStatusCode(
            this PolicyBuilder<HttpResponseMessage> builder
        )
        {
            static bool ResultPredicate(HttpResponseMessage response) => 
                (int) response.StatusCode >= 500 || 
                response.StatusCode == HttpStatusCode.RequestTimeout;

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.OrResult(ResultPredicate);
        }
    }
}