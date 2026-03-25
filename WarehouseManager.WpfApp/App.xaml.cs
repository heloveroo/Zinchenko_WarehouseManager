using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WarehouseManager.Services;

namespace WarehouseManager.WpfApp
{
    /// <summary>
    /// Точка входу застосунку.
    ///
    /// Тут реалізовано Inversion of Control (IoC) через Microsoft.Extensions.DependencyInjection:
    ///   - IWarehouseService реєструється як Singleton (один екземпляр на весь час роботи).
    ///   - MainWindow отримує залежність через конструктор (Constructor Injection).
    ///
    /// Завдяки DI:
    ///   - Код не залежить від конкретних реалізацій (тільки від інтерфейсів).
    ///   - Легко замінити FakeStorage на реальну БД, не змінюючи UI-код.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Глобальний постачальник сервісів.
        /// Зроблений public, щоб сторінки могли отримувати сервіси при потребі.
        /// </summary>
        public static ServiceProvider ServiceProvider { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // ── Реєстрація сервісів у IoC-контейнері ──
            ServiceCollection services = new ServiceCollection();

            // Singleton: один екземпляр сервісу на весь час роботи застосунку.
            // Реєструємо через інтерфейс — це реалізація Dependency Inversion Principle.
            services.AddSingleton<IWarehouseService, WarehouseService>();

            // Transient: нове вікно щоразу (для можливого повторного відкриття)
            services.AddTransient<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();

            // Головне вікно отримуємо через DI-контейнер, а не через new MainWindow()
            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
