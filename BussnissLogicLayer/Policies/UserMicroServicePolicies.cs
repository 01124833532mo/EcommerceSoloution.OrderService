using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace BussnissLogicLayer.Policies
{
    public class UserMicroServicePolicies : IUserMicroServicePolicies
    {
        private readonly ILogger<UserMicroServicePolicies> _logger;

        public UserMicroServicePolicies(ILogger<UserMicroServicePolicies> logger)
        {
            _logger = logger;

        }
        public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            AsyncRetryPolicy<HttpResponseMessage> policy =
                  Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                    .WaitAndRetryAsync(
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        onRetry: (outcome, timespan, retryAttempt, context) =>
                        {
                            _logger.LogInformation("Retrying due to: {Reason}. Waiting {Delay} before next retry. Attempt {RetryAttempt}",
                                outcome.Result?.StatusCode, timespan, retryAttempt);
                        }


                       );
            return policy;


        }
    }
}
