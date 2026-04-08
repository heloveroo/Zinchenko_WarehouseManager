using WarehouseManager.Models.Enums;

namespace WarehouseManager.Models
{
    public class WarehouseModel
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public WarehouseLocation Location { get; set; }

        // Потрібно EF Core для каскадного видалення товарів
        public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();

        protected WarehouseModel() { } // Потрібно EF Core

        public WarehouseModel(int id, string name, WarehouseLocation location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        // Для створення нового складу — ID призначить БД
        public WarehouseModel(string name, WarehouseLocation location)
        {
            Name = name;
            Location = location;
        }
    }
}