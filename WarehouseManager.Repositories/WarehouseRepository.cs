using System.Collections.Generic;
using System.Linq;
using WarehouseManager.Models;
using WarehouseManager.Repositories.Interfaces;
using WarehouseManager.Repositories.Storage;

namespace WarehouseManager.Repositories
{
    /// <summary>Репозиторій для роботи зі складами через FakeStorage.</summary>
    public class WarehouseRepository : IWarehouseRepository
    {
        public IReadOnlyList<WarehouseModel> GetAll() =>
            FakeStorage.Warehouses;

        public WarehouseModel? GetById(int id) =>
            FakeStorage.Warehouses.FirstOrDefault(w => w.Id == id);
    }
}