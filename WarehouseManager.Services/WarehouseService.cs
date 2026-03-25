using System.Collections.Generic;
using WarehouseManager.Models;
using System.Linq;
using WarehouseManager.Repositories.Interfaces;
using WarehouseManager.Services.DTOs;
using WarehouseManager.Services.Interfaces;

namespace WarehouseManager.Services
{
    /// <summary>
    /// Сервіс для роботи зі сховищем даних.
    ///
    /// Відповідає за:
    /// 1. Отримання об'єктів-моделей зі сховища (FakeStorage).
    /// 2. Перетворення моделей зберігання (WarehouseModel / ProductModel)
    ///    у відповідні ViewModel-и для відображення і редагування.
    ///
    /// Тільки цей клас має доступ до FakeStorage.
    /// </summary>
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepo;
        private readonly IProductRepository _productRepo;

        public WarehouseService(
            IWarehouseRepository warehouseRepo,
            IProductRepository productRepo)
        {
            _warehouseRepo = warehouseRepo;
            _productRepo = productRepo;
        }

        public IReadOnlyList<WarehouseListDto> GetAllWarehouses()
        {
            return _warehouseRepo.GetAll()
                .Select(w => new WarehouseListDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Location = w.Location.ToString(),
                    ProductCount = _productRepo.GetByWarehouseId(w.Id).Count
                })
                .ToList();
        }

        public WarehouseDetailDto? GetWarehouseDetail(int warehouseId)
        {
            WarehouseModel? warehouse = _warehouseRepo.GetById(warehouseId);
            if (warehouse is null) return null;

            List<ProductListDto> products = _productRepo
                .GetByWarehouseId(warehouseId)
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category.ToString(),
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice,
                    TotalPrice = p.Quantity * p.UnitPrice
                })
                .ToList();

            return new WarehouseDetailDto
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Location = warehouse.Location.ToString(),
                TotalValue = products.Sum(p => p.TotalPrice),
                Products = products
            };
        }
    }
}
