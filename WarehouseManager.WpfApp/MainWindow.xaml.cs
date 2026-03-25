using System.Windows;
using WarehouseManager.Services;
using WarehouseManager.WpfApp.Pages;

namespace WarehouseManager.WpfApp
{
    /// <summary>
    /// Головне (і єдине) вікно застосунку.
    ///
    /// Реалізує Constructor Injection: IWarehouseService передається ззовні
    /// (через IoC-контейнер у App.xaml.cs), а не створюється всередині.
    /// Це дотримання Dependency Inversion Principle.
    ///
    /// Вікно лише утримує Frame для навігації — вся логіка на сторінках.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IWarehouseService _warehouseService;

        /// <summary>
        /// Constructor Injection: IoC-контейнер автоматично передає IWarehouseService.
        /// </summary>
        /// <param name="warehouseService">Сервіс для роботи зі сховищем (ін'єктується через DI).</param>
        public MainWindow(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
            InitializeComponent();

            // Переходимо на стартову сторінку після ініціалізації вікна
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Передаємо сервіс на стартову сторінку — це Dependency Injection через метод
            MainFrame.Navigate(new WarehouseListPage(_warehouseService, MainFrame));
        }
    }
}
