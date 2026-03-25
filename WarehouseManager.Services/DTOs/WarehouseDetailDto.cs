using System.Collections.Generic;

namespace WarehouseManager.Services.DTOs
{
    /// <summary>DTO для детального перегляду складу — всі поля і список товарів.</summary>
    public class WarehouseDetailDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Location { get; init; } = string.Empty;
        public decimal TotalValue { get; init; }
        public IReadOnlyList<ProductListDto> Products { get; init; } = [];
    }
}