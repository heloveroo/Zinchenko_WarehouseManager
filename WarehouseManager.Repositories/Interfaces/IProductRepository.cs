using WarehouseManager.Models;

namespace WarehouseManager.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductModel?> GetByIdAsync(int id);
        Task<ProductModel> AddAsync(ProductModel product);
        Task UpdateAsync(ProductModel product);
        Task DeleteAsync(int id);
    }
}