using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseManager.Models;
using WarehouseManager.Models.Enums;

namespace WarehouseManager.ViewModels
{
    /// <summary>
    /// Клас для відображення та редагування складу.
    /// На відміну від WarehouseModel, тут присутні:
    /// - список товарів (завантажується окремо, тому може бути null до завантаження),
    /// - обчислюване поле TotalValue.
    /// </summary>
    public class WarehouseViewModel
    {
        public int Id { get; }
        public string Name { get; set; }
        public WarehouseLocation Location { get; set; }

        /// <summary>
        /// Список товарів цього складу.
        /// Може бути null, якщо товари ще не завантажувались (lazy-підхід).
        /// </summary>
        public List<ProductViewModel>? Products { get; set; }

        /// <summary>
        /// Обчислювана загальна вартість усіх товарів на складі.
        /// Повертає 0, якщо товари ще не завантажені.
        /// </summary>
        public decimal TotalValue => Products?.Sum(p => p.TotalPrice) ?? 0m;

        /// <summary>
        /// Конструктор з моделі зберігання.
        /// Товари не передаються — їх треба завантажити окремо.
        /// </summary>
        public WarehouseViewModel(WarehouseModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Location = model.Location;
            Products = null; // ще не завантажені
        }

        /// <summary>
        /// Відображає коротку інформацію про склад у консолі (одним рядком).
        /// </summary>
        public void PrintShort()
        {
            string loaded = Products == null ? "(товари не завантажені)" : $"{Products.Count} товар(ів)";
            Console.WriteLine($"[{Id}] {Name} | {Location} | {loaded}");
        }

        /// <summary>
        /// Відображає детальну інформацію про склад у консолі.
        /// </summary>
        public void PrintDetails()
        {
            Console.WriteLine($"=== Склад: {Name} ===");
            Console.WriteLine($"  ID:           {Id}");
            Console.WriteLine($"  Назва:        {Name}");
            Console.WriteLine($"  Місто:        {Location}");

            if (Products == null)
            {
                Console.WriteLine("  Товари:       не завантажені");
            }
            else
            {
                Console.WriteLine($"  Товарів:      {Products.Count}");
                Console.WriteLine($"  Заг. вартість:{TotalValue:C}");
            }
        }
    }
}
