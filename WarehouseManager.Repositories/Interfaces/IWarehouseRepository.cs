using System.Collections.Generic;
using WarehouseManager.Models;

namespace WarehouseManager.Repositories.Interfaces
{
    /// <summary>Інтерфейс репозиторію складів.</summary>
    public interface IWarehouseRepository
    {
        IReadOnlyList<WarehouseModel> GetAll();
        WarehouseModel? GetById(int id);
    }
}