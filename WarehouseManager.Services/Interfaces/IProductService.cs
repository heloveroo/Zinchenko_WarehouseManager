using System.Threading.Tasks;
using WarehouseManager.Models.Enums;
using WarehouseManager.Services.DTOs;

namespace WarehouseManager.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDetailDto?> GetProductDetailAsync(int productId);
        Task<ProductDetailDto> AddProductAsync(int warehouseId, string name, int quantity,
            decimal unitPrice, ProductCategory category, string description);
        Task UpdateProductAsync(int id, string name, int quantity,
            decimal unitPrice, ProductCategory category, string description);
        Task DeleteProductAsync(int id);
    }
}