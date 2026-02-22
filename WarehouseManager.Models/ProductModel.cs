using WarehouseManager.Models.Enums;

namespace WarehouseManager.Models
{
    /// <summary>
    /// Клас для зберігання даних товару.
    /// Не містить обчислюваних полів (наприклад, TotalPrice) і посилань на об'єкт складу —
    /// тільки «сирі» дані, включно з WarehouseId як зовнішнім ключем.
    /// </summary>
    public class ProductModel
    {
        /// <summary>Унікальний ідентифікатор товару. Тільки для читання після створення.</summary>
        public int Id { get; }

        /// <summary>Ідентифікатор складу, до якого належить товар.</summary>
        public int WarehouseId { get; set; }

        /// <summary>Назва товару.</summary>
        public string Name { get; set; }

        /// <summary>Кількість одиниць на складі.</summary>
        public int Quantity { get; set; }

        /// <summary>Ціна за одну одиницю товару.</summary>
        public decimal UnitPrice { get; set; }

        /// <summary>Категорія товару.</summary>
        public ProductCategory Category { get; set; }

        /// <summary>Опис або технічні характеристики товару.</summary>
        public string Description { get; set; }

        /// <summary>
        /// Основний конструктор — використовується при завантаженні з бази / сховища,
        /// коли id вже відомий.
        /// </summary>
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
    }
}
