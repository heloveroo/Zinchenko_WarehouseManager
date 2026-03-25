using System.Collections.Generic;
using WarehouseManager.ViewModels;

namespace WarehouseManager.Services
{
    /// <summary>
    /// Інтерфейс сервісу для роботи зі сховищем даних.
    /// Використовується для Dependency Inversion та Dependency Injection.
    /// Завдяки інтерфейсу ConsoleApp і WpfApp залежать від абстракції,
    /// а не від конкретної реалізації.
    /// </summary>
    public interface IWarehouseService
    {
        /// <summary>
        /// Повертає список усіх складів.
        /// Товари НЕ завантажуються одразу (lazy-підхід).
        /// </summary>
        List<WarehouseViewModel> GetAllWarehouses();

        /// <summary>
        /// Завантажує товари для вказаного складу.
        /// Також заповнює властивість Products у переданому WarehouseViewModel.
        /// </summary>
        /// <param name="warehouse">ViewModel складу, для якого потрібно завантажити товари.</param>
        List<ProductViewModel> LoadProductsForWarehouse(WarehouseViewModel warehouse);
    }
}
