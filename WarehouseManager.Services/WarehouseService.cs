using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManager.Models;
using WarehouseManager.Models.Enums;
using WarehouseManager.Repositories.Interfaces;
using WarehouseManager.Services.DTOs;
using WarehouseManager.Services.Interfaces;

namespace WarehouseManager.Services
{
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

        public async Task<List<WarehouseListDto>> GetAllWarehousesAsync()
        {
            var warehouses = await _warehouseRepo.GetAllAsync();
            return warehouses.Select(w => new WarehouseListDto
            {
                Id = w.Id,
                Name = w.Name,
                Location = w.Location.ToString(),
                ProductCount = w.Products.Count
            }).ToList();
        }

        public async Task<WarehouseDetailDto?> GetWarehouseDetailAsync(int warehouseId)
        {
            var warehouse = await _warehouseRepo.GetByIdAsync(warehouseId);
            if (warehouse is null) return null;

            var products = warehouse.Products.Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category.ToString(),
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice,
                TotalPrice = p.Quantity * p.UnitPrice
            }).ToList();

            return new WarehouseDetailDto
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Location = warehouse.Location.ToString(),
                TotalValue = products.Sum(p => p.TotalPrice),
                Products = products
            };
        }

        public async Task<WarehouseModel> AddWarehouseAsync(string name, WarehouseLocation location)
        {
            var model = new WarehouseModel(name, location);
            return await _warehouseRepo.AddAsync(model);
        }

        public async Task UpdateWarehouseAsync(int id, string name, WarehouseLocation location)
        {
            var warehouse = await _warehouseRepo.GetByIdAsync(id)
                ?? throw new InvalidOperationException($"Склад з ID {id} не знайдено.");
            warehouse.Name = name;
            warehouse.Location = location;
            await _warehouseRepo.UpdateAsync(warehouse);
        }

        public Task DeleteWarehouseAsync(int id) =>
            _warehouseRepo.DeleteAsync(id);
    }
}