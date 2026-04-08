using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseManager.Models;
using WarehouseManager.Models.Enums;
using WarehouseManager.Services.DTOs;

namespace WarehouseManager.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<List<WarehouseListDto>> GetAllWarehousesAsync();
        Task<WarehouseDetailDto?> GetWarehouseDetailAsync(int warehouseId);
        Task<WarehouseModel> AddWarehouseAsync(string name, WarehouseLocation location);
        Task UpdateWarehouseAsync(int id, string name, WarehouseLocation location);
        Task DeleteWarehouseAsync(int id);
    }
}