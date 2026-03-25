using WarehouseManager.Models;
using WarehouseManager.Repositories.Interfaces;
using WarehouseManager.Services.DTOs;
using WarehouseManager.Services.Interfaces;

namespace WarehouseManager.Services
{
    /// <summary>
    /// Сервіс товарів. Конвертує DB Model у ProductDetailDto.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;

        public ProductService(
            IProductRepository productRepository,
            IWarehouseRepository warehouseRepository)
        {
            _productRepository = productRepository;
            _warehouseRepository = warehouseRepository;
        }

        public ProductDetailDto? GetProductDetail(int productId)
        {
            ProductModel? product = _productRepository.GetById(productId);
            if (product is null) return null;

            WarehouseModel? warehouse = _warehouseRepository.GetById(product.WarehouseId);

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
    }
}