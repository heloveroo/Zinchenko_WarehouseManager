using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WarehouseManager.Services;
using WarehouseManager.ViewModels;

namespace WarehouseManager.WpfApp.Pages
{
    /// <summary>
    /// Сторінка 1: список усіх складів.
    ///
    /// Отримує IWarehouseService через конструктор (Constructor Injection).
    /// Навігація відбувається через переданий Frame — жодних нових вікон.
    /// </summary>
    public partial class WarehouseListPage : Page
    {
        private readonly IWarehouseService _warehouseService;
        private readonly Frame _navigationFrame;
        private List<WarehouseViewModel> _warehouses = new();

        /// <summary>
        /// Constructor Injection: сервіс і фрейм передаються ззовні (з MainWindow).
        /// </summary>
        public WarehouseListPage(IWarehouseService warehouseService, Frame navigationFrame)
        {
            _warehouseService = warehouseService;
            _navigationFrame = navigationFrame;
            InitializeComponent();
            LoadWarehouses();
        }

        /// <summary>
        /// Завантажує список складів зі сховища і прив'язує до ItemsControl.
        /// </summary>
        private void LoadWarehouses()
        {
            _warehouses = _warehouseService.GetAllWarehouses();

            WarehouseList.ItemsSource = _warehouses;
            SubtitleText.Text = $"Знайдено складів: {_warehouses.Count}";
        }

        /// <summary>
        /// Обробник кліку на картку складу — перехід на сторінку деталей.
        /// ViewModel передається на наступну сторінку як параметр навігації.
        /// </summary>
        private void OnWarehouseClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is WarehouseViewModel warehouse)
            {
                _navigationFrame.Navigate(
                    new WarehouseDetailPage(_warehouseService, _navigationFrame, warehouse));
            }
        }
    }
}
