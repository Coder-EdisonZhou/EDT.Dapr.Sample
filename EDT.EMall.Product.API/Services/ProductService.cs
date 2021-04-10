namespace EDT.EMall.Product.API.Services
{
    public class ProductService : IProductService
    {
        private const int ProductStockSum = 100;

        public Models.Product GetProductById(int productId)
        {
            return new Models.Product()
            {
                Id = 1000,
                Name = "HuaWei Mate30 Pro",
                Stock = ProductStockSum
            };
        }

        public bool SaveProduct(Models.Product product)
        {
            return true;
        }
    }
}
