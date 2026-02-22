using System;
using System.Collections.Generic;
using System.Linq;
using WarehouseManager.Services;
using WarehouseManager.ViewModels;

namespace WarehouseManager.ConsoleApp
{
    /// <summary>
    /// Консольний застосунок "Менеджер товарів".
    ///
    /// Логіка навігації:
    ///   1. При запуску завантажується список складів і виводиться користувачу.
    ///   2. Користувач обирає склад за номером — бачить деталі і список товарів.
    ///   3. Якщо товарів багато, можна переглянути повну інформацію по кожному.
    ///   4. Команда "0" повертає до списку складів.
    ///   5. Команда "exit" завершує застосунок.
    /// </summary>
    internal class Program
    {
        // Сервіс — єдиний спосіб дістатись до сховища
        private static readonly WarehouseService _service = new WarehouseService();

        // Список складів завантажується один раз і зберігається між кроками
        private static List<WarehouseViewModel> _warehouses = new List<WarehouseViewModel>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            PrintHeader();

            // Завантаження складів
            _warehouses = _service.GetAllWarehouses();

            bool running = true;

            while (running)
            {
                ShowWarehouseList();
                string input = PromptUser("Введіть номер складу або 'exit' для виходу");

                if (input.ToLower() == "exit")
                {
                    running = false;
                    Console.WriteLine("\nДо побачення!");
                    break;
                }

                if (int.TryParse(input, out int warehouseId))
                {
                    WarehouseViewModel? selected = _warehouses.FirstOrDefault(w => w.Id == warehouseId);

                    if (selected == null)
                    {
                        PrintError($"Склад з ID '{warehouseId}' не знайдено.");
                    }
                    else
                    {
                        ShowWarehouseDetails(selected);
                    }
                }
                else
                {
                    PrintError("Невірний ввід. Введіть числовий ID або 'exit'.");
                }
            }
        }

        // ---------------------------------------------------------------
        // Відображення списку складів
        // ---------------------------------------------------------------
        private static void ShowWarehouseList()
        {
            PrintSeparator();
            Console.WriteLine("  СПИСОК СКЛАДІВ");
            PrintSeparator();

            foreach (WarehouseViewModel warehouse in _warehouses)
            {
                warehouse.PrintShort();
            }

            PrintSeparator();
        }

        // ---------------------------------------------------------------
        // Відображення деталей складу і його товарів
        // ---------------------------------------------------------------
        private static void ShowWarehouseDetails(WarehouseViewModel warehouse)
        {
            // Завантажуємо товари лише якщо ще не завантажені (lazy loading)
            if (warehouse.Products == null)
            {
                Console.WriteLine("\nЗавантаження товарів...");
                _service.LoadProductsForWarehouse(warehouse);
            }

            PrintSeparator();
            warehouse.PrintDetails();
            PrintSeparator();

            if (warehouse.Products == null || warehouse.Products.Count == 0)
            {
                Console.WriteLine("  На цьому складі немає товарів.");
                WaitForEnter();
                return;
            }

            // Виводимо короткий список товарів
            Console.WriteLine("\n  ТОВАРИ НА СКЛАДІ:");
            foreach (ProductViewModel product in warehouse.Products)
            {
                product.PrintShort();
            }
            PrintSeparator();

            // Підменю складу
            bool inWarehouse = true;
            while (inWarehouse)
            {
                string input = PromptUser(
                    "Введіть ID товару для деталей, '0' — повернутись до списку складів");

                if (input == "0")
                {
                    inWarehouse = false;
                }
                else if (int.TryParse(input, out int productId))
                {
                    ProductViewModel? product = warehouse.Products
                        .FirstOrDefault(p => p.Id == productId);

                    if (product == null)
                    {
                        PrintError($"Товар з ID '{productId}' не знайдено на цьому складі.");
                    }
                    else
                    {
                        Console.WriteLine();
                        product.PrintDetails();
                        PrintSeparator();
                    }
                }
                else
                {
                    PrintError("Невірний ввід. Введіть числовий ID або '0'.");
                }
            }
        }

        // ---------------------------------------------------------------
        // Допоміжні методи для консольного виводу
        // ---------------------------------------------------------------

        private static void PrintHeader()
        {
            Console.Clear();
            PrintSeparator('=');
            Console.WriteLine("       МЕНЕДЖЕР ТОВАРІВ — Лабораторна робота 1");
            PrintSeparator('=');
            Console.WriteLine();
        }

        private static void PrintSeparator(char ch = '-')
        {
            Console.WriteLine(new string(ch, 55));
        }

        private static string PromptUser(string message)
        {
            Console.Write($"\n> {message}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  [!] {message}");
            Console.ResetColor();
        }

        private static void WaitForEnter()
        {
            Console.WriteLine("\nНатисніть Enter для продовження...");
            Console.ReadLine();
        }
    }
}
