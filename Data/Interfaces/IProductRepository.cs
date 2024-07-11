using Common.Models;

namespace Data.Interfaces
{
    /// <summary>
    /// Interface Product Repo
    /// </summary>
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
