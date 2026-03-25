using System.Collections.Generic;
using System.Linq;
using WarehouseManager.Models;
using WarehouseManager.Repositories.Interfaces;
using WarehouseManager.Repositories.Storage;

namespace WarehouseManager.Repositories
{
    /// <summary>Репозиторій для роботи з товарами через FakeStorage.</summary>
    public class ProductRepository : IProductRepository
    {
        public IReadOnlyList<ProductModel> GetByWarehouseId(int warehouseId) =>
            FakeStorage.Products.Where(p => p.WarehouseId == warehouseId).ToList();

        public ProductModel? GetById(int id) =>
            FakeStorage.Products.FirstOrDefault(p => p.Id == id);
    }
}