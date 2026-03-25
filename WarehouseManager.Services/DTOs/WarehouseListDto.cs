namespace WarehouseManager.Services.DTOs
{
    /// <summary>DTO для відображення складу в списку.</summary>
    public class WarehouseListDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Location { get; init; } = string.Empty;
        public int ProductCount { get; init; }
    }
}