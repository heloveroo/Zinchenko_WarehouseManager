using System;
using WarehouseManager.Models;
using WarehouseManager.Models.Enums;

namespace WarehouseManager.ViewModels
{
    /// <summary>
    /// Клас для відображення та редагування товару.
    /// На відміну від ProductModel, тут присутнє обчислюване поле TotalPrice.
    /// </summary>
    public class ProductViewModel
    {
        public int Id { get; }
        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Обчислювана загальна вартість товару = ціна × кількість.
        /// </summary>
        public decimal TotalPrice => Quantity * UnitPrice;

        /// <summary>
        /// Конструктор з моделі зберігання.
        /// </summary>
        public ProductViewModel(ProductModel model)
        {
            Id = model.Id;
            WarehouseId = model.WarehouseId;
            Name = model.Name;
            Quantity = model.Quantity;
            UnitPrice = model.UnitPrice;
            Category = model.Category;
            Description = model.Description;
        }

        /// <summary>
        /// Відображає коротку інформацію про товар (один рядок).
        /// </summary>
        public void PrintShort()
        {
            Console.WriteLine($"  [{Id}] {Name} | {Category} | {Quantity} шт. × {UnitPrice:C} = {TotalPrice:C}");
        }

        /// <summary>
        /// Відображає повну інформацію про товар.
        /// </summary>
        public void PrintDetails()
        {
            Console.WriteLine($"  --- Товар: {Name} ---");
            Console.WriteLine($"    ID:           {Id}");
            Console.WriteLine($"    Склад ID:     {WarehouseId}");
            Console.WriteLine($"    Категорія:    {Category}");
            Console.WriteLine($"    Кількість:    {Quantity} шт.");
            Console.WriteLine($"    Ціна/шт:      {UnitPrice:C}");
            Console.WriteLine($"    Заг. вартість:{TotalPrice:C}");
            Console.WriteLine($"    Опис:         {Description}");
        }
    }
}
