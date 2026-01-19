using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace Seguro.Hexagonal.Contratacao.IoC.Resilience;

public static class HttpPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> RetryPolicy =>
        HttpPolicyExtensions
            .HandleTransientHttpError()            
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retry =>
                    TimeSpan.FromSeconds(Math.Pow(2, retry))
            );

    public static IAsyncPolicy<HttpResponseMessage> TimeoutPolicy =>
        Policy.TimeoutAsync<HttpResponseMessage>(
            TimeSpan.FromSeconds(5)
        );
}
