using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Orion.OperationResult.Implementations;
using Orion.Resilience.Builders;
using Orion.Resilience.Settings;
using Polly.Wrap;

namespace Orion.Resilience.Resources
{
    public abstract class ApiResourceBase
    {
        private static readonly ConcurrentDictionary<string, AsyncPolicyWrap<HttpResponseMessage>> Policies = new();

        private readonly HttpClient _client;

        protected ApiResourceBase(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _client = client;
        }

        protected async Task<ApiResult<TResult, TError>> SendAsync<TResult, TError>(
            ResilienceStrategySettings settings,
            Func<HttpRequestMessage> creator
        ) where TResult : class =>
            await SendAsync<TResult, TError>(settings, creator, null);

        protected async Task<ApiResult<TResult, TError>> SendAsync<TCommand, TResult, TError>(
            ResilienceStrategySettings settings,
            Func<HttpRequestMessage> creator,
            TCommand command
        ) where TResult : class
        {
            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await SendAsync<TResult, TError>(settings, creator, content);
        }

        private async Task<ApiResult<TResult, TError>> SendAsync<TResult, TError>(
            ResilienceStrategySettings settings,
            Func<HttpRequestMessage> creator,
            HttpContent content
        ) where TResult : class
        {
            HttpRequestMessage CreateRequest()
            {
                var request = creator.Invoke();
                request.Content = content;
                return request;
            }

            Task<HttpResponseMessage> CallAsync()
            {
                var request = CreateRequest();
                return _client.SendAsync(request);
            }

            using var response = await ConfigurePolicy(settings).ExecuteAsync(CallAsync);
                
            if (response.IsSuccessStatusCode)
                return await ApiResult<TResult, TError>.SuccessAsync(response);

            return await ApiResult<TResult, TError>.FailureAsync(response);
        }
        
        private static AsyncPolicyWrap<HttpResponseMessage> ConfigurePolicy(ResilienceStrategySettings settings) =>
            Policies.GetOrAdd(
                settings.Key,
                HttpResilienceStrategyBuilder.Build(
                    settings.RetrySleeps,
                    settings.EventsBeforeBreaking,
                    settings.DurationOfBreak,
                    settings.Timeout
                )
            );
    }
}