using System.Collections.Generic;
using WarehouseManager.Services.DTOs;

namespace WarehouseManager.Services.Interfaces
{
    /// <summary>Інтерфейс сервісу складів.</summary>
    public interface IWarehouseService
    {
        IReadOnlyList<WarehouseListDto> GetAllWarehouses();
        WarehouseDetailDto? GetWarehouseDetail(int warehouseId);
    }
}
