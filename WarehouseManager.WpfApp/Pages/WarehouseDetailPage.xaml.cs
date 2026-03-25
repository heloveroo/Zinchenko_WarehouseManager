using System.Windows;
using System.Windows.Controls;
using WarehouseManager.Services;
using WarehouseManager.ViewModels;

namespace WarehouseManager.WpfApp.Pages
{
    /// <summary>
    /// Сторінка 2: деталі конкретного складу та список його товарів.
    ///
    /// Отримує:
    ///  - IWarehouseService через конструктор (DI) — для завантаження товарів;
    ///  - Frame — для навігації вперед (на сторінку товару) та назад;
    ///  - WarehouseViewModel — обраний склад (передається з попередньої сторінки).
    /// </summary>
    public partial class WarehouseDetailPage : Page
    {
        private readonly IWarehouseService _warehouseService;
        private readonly Frame _navigationFrame;
        private readonly WarehouseViewModel _warehouse;

        public WarehouseDetailPage(
            IWarehouseService warehouseService,
            Frame navigationFrame,
            WarehouseViewModel warehouse)
        {
            _warehouseService = warehouseService;
            _navigationFrame = navigationFrame;
            _warehouse = warehouse;

            InitializeComponent();
            LoadPage();
        }

        /// <summary>
        /// Заповнює сторінку даними складу і завантажує товари (lazy loading).
        /// </summary>
        private void LoadPage()
        {
            // ── Дані складу ──
            WarehouseNameText.Text     = _warehouse.Name;
            WarehouseLocationText.Text = _warehouse.Location.ToString();
            WarehouseIdText.Text       = _warehouse.Id.ToString();

            // ── Завантаження товарів якщо ще не завантажені ──
            if (_warehouse.Products == null)
            {
                _warehouseService.LoadProductsForWarehouse(_warehouse);
            }

            // ── Відображення загальної вартості (після завантаження товарів) ──
            TotalValueText.Text = _warehouse.TotalValue.ToString("C");

            // ── Список товарів ──
            if (_warehouse.Products == null || _warehouse.Products.Count == 0)
            {
                // Склад порожній
                ProductsHeaderText.Text        = "Товари";
                ProductsScrollViewer.Visibility = Visibility.Collapsed;
                EmptyPanel.Visibility           = Visibility.Visible;
            }
            else
            {
                ProductsHeaderText.Text = $"Товари ({_warehouse.Products.Count})";
                ProductList.ItemsSource = _warehouse.Products;
            }
        }

        /// <summary>
        /// Повернення на сторінку зі списком складів.
        /// </summary>
        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            if (_navigationFrame.CanGoBack)
            {
                _navigationFrame.GoBack();
            }
        }

        /// <summary>
        /// Перехід на сторінку деталей обраного товару.
        /// </summary>
        private void OnProductClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ProductViewModel product)
            {
                _navigationFrame.Navigate(
                    new ProductDetailPage(_navigationFrame, product));
            }
        }
    }
}
