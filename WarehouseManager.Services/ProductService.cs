using System;
using System.Threading.Tasks;
using WarehouseManager.Models;
using WarehouseManager.Models.Enums;
using WarehouseManager.Repositories.Interfaces;
using WarehouseManager.Services.DTOs;
using WarehouseManager.Services.Interfaces;

namespace WarehouseManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IWarehouseRepository _warehouseRepo;

        public ProductService(
            IProductRepository productRepo,
            IWarehouseRepository warehouseRepo)
        {
            _productRepo = productRepo;
            _warehouseRepo = warehouseRepo;
        }

        public async Task<ProductDetailDto?> GetProductDetailAsync(int productId)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product is null) return null;

            var warehouse = await _warehouseRepo.GetByIdAsync(product.WarehouseId);

            return new ProductDetailDto
            {
                Id = product.Id,
                WarehouseId = product.WarehouseId,
                WarehouseName = warehouse?.Name ?? "—",
                Name = product.Name,
                Category = product.Category.ToString(),
                Quantity = product.Quantity,
                UnitPrice = product.UnitPrice,
                TotalPrice = product.Quantity * product.UnitPrice,
                Description = product.Description
            };
        }

        public async Task<ProductDetailDto> AddProductAsync(int warehouseId, string name,
            int quantity, decimal unitPrice, ProductCategory category, string description)
        {
            var model = new ProductModel(warehouseId, name, quantity, unitPrice, category, description);
            var created = await _productRepo.AddAsync(model);

            var warehouse = await _warehouseRepo.GetByIdAsync(warehouseId);

            return new ProductDetailDto
            {
                Id = created.Id,
                WarehouseId = created.WarehouseId,
                WarehouseName = warehouse?.Name ?? "—",
                Name = created.Name,
                Category = created.Category.ToString(),
                Quantity = created.Quantity,
                UnitPrice = created.UnitPrice,
                TotalPrice = created.Quantity * created.UnitPrice,
                Description = created.Description
            };
        }

        public async Task UpdateProductAsync(int id, string name, int quantity,
            decimal unitPrice, ProductCategory category, string description)
        {
            var product = await _productRepo.GetByIdAsync(id)
                ?? throw new InvalidOperationException($"Товар з ID {id} не знайдено.");
            product.Name = name;
            product.Quantity = quantity;
            product.UnitPrice = unitPrice;
            product.Category = category;
            product.Description = description;
            await _productRepo.UpdateAsync(product);
        }

        public Task DeleteProductAsync(int id) =>
            _productRepo.DeleteAsync(id);
    }
}