using BussnissLogicLayer.DTO;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Fallback;
using System.Text;
using System.Text.Json;

namespace BussnissLogicLayer.Policies
{
    public class ProductMicroservicePolicies : IProductMicroservicePolicies
    {
        private readonly ILogger<ProductMicroservicePolicies> _logger;

        public ProductMicroservicePolicies(ILogger<ProductMicroservicePolicies> logger)
        {
            _logger = logger;
        }


        public IAsyncPolicy<HttpResponseMessage> GetFallBackPollicy()
        {
            AsyncFallbackPolicy<HttpResponseMessage> policy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
              .FallbackAsync(async (context) =>
              {
                  _logger.LogWarning("Fallback triggered: The request failed, returning dummy data");

                  ProductDTO product = new ProductDTO(ProductID: Guid.Empty,
                            ProductName: "Temporarily Unavailable (fallback)",
                            Category: "Temporarily Unavailable (fallback)",
                            UnitPrice: 0,
                            QuantityInStock: 0
                            );

                  var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                  {
                      Content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json")
                  };

                  return response;
              });

            return policy;
        }
    }
}
