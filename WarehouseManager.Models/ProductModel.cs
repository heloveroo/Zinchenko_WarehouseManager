using WarehouseManager.Models.Enums;

namespace WarehouseManager.Models
{
    public class ProductModel
    {
        public int Id { get; private set; }
        public int WarehouseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; } = string.Empty;

        // Навігаційна властивість — посилання на склад
        public virtual WarehouseModel? Warehouse { get; set; }

        protected ProductModel() { } // Потрібно EF Core

        public ProductModel(int id, int warehouseId, string name, int quantity,
            decimal unitPrice, ProductCategory category, string description)
        {
            Id = id;
            WarehouseId = warehouseId;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Category = category;
            Description = description;
        }

        // Для створення нового товару — ID призначить БД
        public ProductModel(int warehouseId, string name, int quantity,
            decimal unitPrice, ProductCategory category, string description)
        {
            WarehouseId = warehouseId;
            Name = name;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Category = category;
            Description = description;
        }
    }
}