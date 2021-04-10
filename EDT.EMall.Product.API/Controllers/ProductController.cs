using Dapr;
using EDT.EMall.Product.API.Models;
using EDT.EMall.Product.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDT.EMall.Product.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private const string DaprPubSubName = "pubsub";

        private static readonly string[] FakeProducts = new[]
        {
            "SKU1", "SKU2", "SKU3", "SKU4", "SKU5", "SKU6", "SKU7", "SKU8", "SKU9", "SKU10"
        };

        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<SKU> Get()
        {
            _logger.LogInformation("[Begin] Query product data.");

            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new SKU
            {
                Date = DateTime.Now.AddDays(index),
                Index = rng.Next(1, 100),
                Summary = FakeProducts[rng.Next(FakeProducts.Length)]
            })
            .ToArray();

            _logger.LogInformation("[End] Query product data.");

            return result;
        }
        
        [HttpPost]
        [Topic(DaprPubSubName, "neworder")]
        public Models.Product SubProductStock(OrderStockDto orderStockDto)
        {
            _logger.LogInformation($"[Begin] Sub Product Stock, Stock Need : {orderStockDto.Count}.");

            var product = _productService.GetProductById(orderStockDto.ProductId);
            if (orderStockDto.Count < 0 || orderStockDto.Count > product.Stock)
            {
                throw new InvalidOperationException("Invalid Product Count!");
            }
            product.Stock = product.Stock - orderStockDto.Count;
            _productService.SaveProduct(product);

            _logger.LogInformation($"[End] Sub Product Stock Finished, Stock Now : {product.Stock}.");

            return product;
        }
    }
}
