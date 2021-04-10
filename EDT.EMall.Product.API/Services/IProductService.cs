namespace EDT.EMall.Product.API.Services
{
    public interface IProductService
    {
        Models.Product GetProductById(int productId);

        bool SaveProduct(Models.Product product);
    }
}
