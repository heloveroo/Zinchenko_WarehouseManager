using WarehouseManager.Services.DTOs;

namespace WarehouseManager.Services.Interfaces
{
    /// <summary>Інтерфейс сервісу товарів.</summary>
    public interface IProductService
    {
        ProductDetailDto? GetProductDetail(int productId);
    }
}