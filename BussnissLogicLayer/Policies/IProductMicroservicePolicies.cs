using Polly;

namespace BussnissLogicLayer.Policies
{
    public interface IProductMicroservicePolicies
    {
        IAsyncPolicy<HttpResponseMessage> GetFallBackPollicy();
    }
}
