using System.Windows;
using System.Windows.Controls;
using WarehouseManager.ViewModels;

namespace WarehouseManager.WpfApp.Pages
{
    /// <summary>
    /// Сторінка 3: повна інформація про конкретний товар.
    ///
    /// Отримує:
    ///  - Frame — для навігації назад;
    ///  - ProductViewModel — обраний товар (передається з WarehouseDetailPage).
    ///
    /// Ця сторінка не потребує сервісу — всі дані вже є у ViewModel.
    /// </summary>
    public partial class ProductDetailPage : Page
    {
        private readonly Frame _navigationFrame;

        public ProductDetailPage(Frame navigationFrame, ProductViewModel product)
        {
            _navigationFrame = navigationFrame;
            InitializeComponent();
            BindProduct(product);
        }

        /// <summary>
        /// Заповнює всі поля сторінки даними з ProductViewModel.
        /// </summary>
        private void BindProduct(ProductViewModel product)
        {
            CategoryText.Text       = product.Category.ToString();
            ProductNameText.Text    = product.Name;
            ProductIdText.Text      = $"ID: {product.Id}";
            TotalPriceText.Text     = product.TotalPrice.ToString("C");
            QuantityText.Text       = $"{product.Quantity} шт.";
            UnitPriceText.Text      = product.UnitPrice.ToString("C");
            WarehouseIdText.Text    = product.WarehouseId.ToString();
            CategoryDetailText.Text = product.Category.ToString();
            DescriptionText.Text    = product.Description;
        }

        /// <summary>
        /// Повернення на сторінку деталей складу.
        /// </summary>
        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            if (_navigationFrame.CanGoBack)
            {
                _navigationFrame.GoBack();
            }
        }
    }
}
