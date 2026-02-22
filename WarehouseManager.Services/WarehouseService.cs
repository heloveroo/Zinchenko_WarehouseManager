using System.Collections.Generic;
using WarehouseManager.Models;
using WarehouseManager.Services.Storage;
using WarehouseManager.ViewModels;

namespace WarehouseManager.Services
{
    /// <summary>
    /// Сервіс для роботи зі сховищем даних.
    ///
    /// Відповідає за:
    /// 1. Отримання об'єктів-моделей зі сховища (FakeStorage).
    /// 2. Перетворення моделей зберігання (WarehouseModel / ProductModel)
    ///    у відповідні ViewModel-и для відображення і редагування.
    ///
    /// Тільки цей клас має доступ до FakeStorage.
    /// </summary>
    public class WarehouseService
    {
        /// <summary>
        /// Повертає список усіх складів як WarehouseViewModel.
        /// Товари НЕ завантажуються одразу — Products залишається null.
        /// </summary>
        public List<WarehouseViewModel> GetAllWarehouses()
        {
            List<WarehouseViewModel> result = new List<WarehouseViewModel>();

            foreach (WarehouseModel model in FakeStorage.Warehouses)
            {
                result.Add(new WarehouseViewModel(model));
            }

            return result;
        }

        /// <summary>
        /// Завантажує і повертає список товарів для вказаного складу.
        /// Також заповнює властивість Products у переданому WarehouseViewModel.
        /// </summary>
        /// <param name="warehouse">ViewModel складу, для якого потрібно завантажити товари.</param>
        public List<ProductViewModel> LoadProductsForWarehouse(WarehouseViewModel warehouse)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

            foreach (ProductModel model in FakeStorage.Products)
            {
                if (model.WarehouseId == warehouse.Id)
                {
                    products.Add(new ProductViewModel(model));
                }
            }

            // Зберігаємо в ViewModel, щоб не завантажувати повторно
            warehouse.Products = products;

            return products;
        }
    }
}
