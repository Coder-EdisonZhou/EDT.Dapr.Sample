using Dapr.Client;
using EDT.EMall.Cart.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EDT.EMall.Cart.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly DaprClient _daprClient;

        public CartController(ILogger<CartController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet]
        public async Task<IEnumerable<SKU>> Get()
        {
            _logger.LogInformation("[Begin] Query product data from Product Service");

            var products = await _daprClient.InvokeMethodAsync<IEnumerable<SKU>>
                (HttpMethod.Get, "ProductService", "Product");

            _logger.LogInformation($"[End] Query product data from Product Service, data : {products.ToArray().ToString()}");

            return products;
        }
    }
}
