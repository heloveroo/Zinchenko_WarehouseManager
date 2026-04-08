using Microsoft.EntityFrameworkCore;
using WarehouseManager.Models;
using WarehouseManager.Models.Enums;

namespace WarehouseManager.Repositories
{
    public static class DbInitializer
    {
        /// <summary>
        /// Створює БД якщо не існує і заповнює початковими даними лише при першому запуску.
        /// </summary>
        public static async Task InitializeAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            // Якщо дані вже є — нічого не робимо
            if (await context.Warehouses.AnyAsync()) return;

            // ---- Склади -------------------------------------------------------
            var central = new WarehouseModel("Центральний", WarehouseLocation.Kyiv);
            var western = new WarehouseModel("Західний", WarehouseLocation.Lviv);
            var bohuslaw = new WarehouseModel("Богуславський", WarehouseLocation.Bohuslaw);

            await context.Warehouses.AddRangeAsync(central, western, bohuslaw);
            await context.SaveChangesAsync();

            // ---- Товари -------------------------------------------------------
            var products = new ProductModel[]
            {
                new(central.Id, "Ноутбук Dell XPS 15",        5,  42000m, ProductCategory.Electronics, "Intel Core i7, 16 GB RAM, 512 GB SSD, 15.6\" FHD"),
                new(central.Id, "Смартфон Samsung Galaxy S23", 12, 28000m, ProductCategory.Electronics, "6.1\", 256 GB, Android 13"),
                new(central.Id, "Навушники Sony WH-1000XM5",   8,  12500m, ProductCategory.Electronics, "Бездротові, активне шумозаглушення"),
                new(central.Id, "Куртка зимова чоловіча",      20,  3200m, ProductCategory.Clothing,    "Розміри S–XXL, колір: чорний, темно-синій"),
                new(central.Id, "Джинси Levi's 501",           30,  2100m, ProductCategory.Clothing,    "Класичний прямий крій, розміри 28–36"),
                new(central.Id, "Рис пропарений 1 кг",        100,    55m, ProductCategory.Food,        "Пакет 1 кг, ДСТУ, термін придатності 12 міс."),
                new(central.Id, "Оливкова олія 0.5 л",         60,   180m, ProductCategory.Food,        "Extra Virgin, Іспанія, скляна пляшка"),
                new(central.Id, "Офісний стіл 120×60",          4,  5800m, ProductCategory.Furniture,   "ДСП, білий, регульована висота"),
                new(central.Id, "Крісло офісне Comfort Pro",    6,  7200m, ProductCategory.Furniture,   "Сітчаста спинка, підлокітники, підголовник"),
                new(central.Id, "Перфоратор Bosch GBH 2-26",    3,  6400m, ProductCategory.Tools,       "800 Вт, SDS+, 3 режими роботи"),
                new(western.Id, "Планшет iPad Air 5",           7, 22000m, ProductCategory.Electronics, "10.9\", M1, Wi-Fi, 64 GB"),
                new(western.Id, "Футболка Polo Ralph Lauren",   15,  1800m, ProductCategory.Clothing,   "100% бавовна, розміри S–XL, різні кольори"),
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}