using System.Collections.Generic;
using WarehouseManager.Models;
using WarehouseManager.Models.Enums;

namespace WarehouseManager.Services.Storage
{
    /// <summary>
    /// Штучне сховище даних — замінює реальну базу даних на початковому етапі.
    /// Клас статичний: дані ініціалізуються один раз і доступні весь час роботи застосунку.
    ///
    /// ДОСТУП до цього класу повинен бути ТІЛЬКИ через WarehouseService.
    /// Тому клас має модифікатор internal — він не видимий поза межами проєкту Services.
    /// </summary>
    internal static class FakeStorage
    {
        // ---- Склади -------------------------------------------------------
        internal static readonly List<WarehouseModel> Warehouses = new List<WarehouseModel>
        {
            new WarehouseModel(1, "Центральний",  WarehouseLocation.Kyiv),
            new WarehouseModel(2, "Західний",     WarehouseLocation.Lviv),
            new WarehouseModel(3, "Богуславський",WarehouseLocation.Bohuslaw),
        };

        // ---- Товари -------------------------------------------------------
        // Склад 1 (Центральний, Київ) — 10 товарів
        // Склад 2 (Західний, Львів)  —  2 товари
        // Склад 3 (Богуслав)         —  0 товарів (порожній склад для демонстрації)
        internal static readonly List<ProductModel> Products = new List<ProductModel>
        {
            // Склад 1 — Центральний (Київ)
            new ProductModel(1,  1, "Ноутбук Dell XPS 15",        5,  42000m, ProductCategory.Electronics,
                "Intel Core i7, 16 GB RAM, 512 GB SSD, 15.6\" FHD"),
            new ProductModel(2,  1, "Смартфон Samsung Galaxy S23", 12, 28000m, ProductCategory.Electronics,
                "6.1\", 256 GB, Android 13"),
            new ProductModel(3,  1, "Навушники Sony WH-1000XM5",  8,  12500m, ProductCategory.Electronics,
                "Бездротові, активне шумозаглушення"),
            new ProductModel(4,  1, "Куртка зимова чоловіча",     20,  3200m, ProductCategory.Clothing,
                "Розміри S–XXL, колір: чорний, темно-синій"),
            new ProductModel(5,  1, "Джинси Levi's 501",          30,  2100m, ProductCategory.Clothing,
                "Класичний прямий крій, розміри 28–36"),
            new ProductModel(6,  1, "Рис пропарений 1 кг",       100,    55m, ProductCategory.Food,
                "Пакет 1 кг, ДСТУ, термін придатності 12 міс."),
            new ProductModel(7,  1, "Оливкова олія 0.5 л",        60,   180m, ProductCategory.Food,
                "Extra Virgin, Іспанія, скляна пляшка"),
            new ProductModel(8,  1, "Офісний стіл 120×60",         4,  5800m, ProductCategory.Furniture,
                "ДСП, білий, регульована висота"),
            new ProductModel(9,  1, "Крісло офісне Comfort Pro",   6,  7200m, ProductCategory.Furniture,
                "Сітчаста спинка, підлокітники, підголовник"),
            new ProductModel(10, 1, "Перфоратор Bosch GBH 2-26",   3,  6400m, ProductCategory.Tools,
                "800 Вт, SDS+, 3 режими роботи"),

            // Склад 2 — Західний (Львів)
            new ProductModel(11, 2, "Планшет iPad Air 5",          7, 22000m, ProductCategory.Electronics,
                "10.9\", M1, Wi-Fi, 64 GB"),
            new ProductModel(12, 2, "Футболка Polo Ralph Lauren",  15,  1800m, ProductCategory.Clothing,
                "100% бавовна, розміри S–XL, різні кольори"),
        };
    }
}
