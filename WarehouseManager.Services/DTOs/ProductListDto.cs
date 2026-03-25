namespace WarehouseManager.Services.DTOs
{
    /// <summary>DTO для відображення товару в списку складу.</summary>
    public class ProductListDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal TotalPrice { get; init; }
    }
}