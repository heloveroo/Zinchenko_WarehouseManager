using WarehouseManager.Models;

namespace WarehouseManager.Repositories.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<List<WarehouseModel>> GetAllAsync();
        Task<WarehouseModel?> GetByIdAsync(int id);
        Task<WarehouseModel> AddAsync(WarehouseModel warehouse);
        Task UpdateAsync(WarehouseModel warehouse);
        Task DeleteAsync(int id);
    }
}