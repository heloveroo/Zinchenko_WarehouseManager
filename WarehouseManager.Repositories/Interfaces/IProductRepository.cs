using System.Collections.Generic;
using WarehouseManager.Models;

namespace WarehouseManager.Repositories.Interfaces
{
    /// <summary>Інтерфейс репозиторію товарів.</summary>
    public interface IProductRepository
    {
        IReadOnlyList<ProductModel> GetByWarehouseId(int warehouseId);
        ProductModel? GetById(int id);
    }
}