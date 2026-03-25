namespace WarehouseManager.Services.DTOs
{
    /// <summary>DTO для детального перегляду товару — всі поля включно з назвою складу.</summary>
    public class ProductDetailDto
    {
        public int Id { get; init; }
        public int WarehouseId { get; init; }
        public string WarehouseName { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal TotalPrice { get; init; }
        public string Description { get; init; } = string.Empty;
    }
}