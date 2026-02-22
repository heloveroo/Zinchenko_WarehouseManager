using WarehouseManager.Models.Enums;

namespace WarehouseManager.Models
{
    /// <summary>
    /// Клас для зберігання даних складу.
    /// Не містить обчислюваних полів і колекцій товарів —
    /// тільки «сирі» дані, які зберігаються у сховищі.
    /// </summary>
    public class WarehouseModel
    {
        /// <summary>Унікальний ідентифікатор складу. Тільки для читання після створення.</summary>
        public int Id { get; }

        /// <summary>Назва складу, наприклад «Центральний» або «Склад №2».</summary>
        public string Name { get; set; }

        /// <summary>Місто, де знаходиться склад.</summary>
        public WarehouseLocation Location { get; set; }

        /// <summary>
        /// Основний конструктор — використовується при завантаженні з бази / сховища,
        /// коли id вже відомий.
        /// </summary>
        public WarehouseModel(int id, string name, WarehouseLocation location)
        {
            Id = id;
            Name = name;
            Location = location;
        }
    }
}
