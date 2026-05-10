using Polly;

namespace BussnissLogicLayer.Policies
{
    public interface IUserMicroServicePolicies
    {
        IAsyncPolicy<HttpResponseMessage> GetRetryPolicy();
        IAsyncPolicy<HttpResponseMessage> GetCirCuitBreakerPolicy();
    }
}
