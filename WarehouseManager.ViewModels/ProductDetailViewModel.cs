using System.Windows.Input;
using WarehouseManager.Services.DTOs;
using WarehouseManager.Services.Interfaces;
using WarehouseManager.ViewModels.Navigation;
using WarehouseManager.ViewModels;

namespace WarehouseManager.ViewModels
{
    /// <summary>ViewModel сторінки з повною інформацією про товар.</summary>
    public class ProductDetailViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly INavigationService _navigation;

        private ProductDetailDto? _product;
        private int _warehouseId;

        public ProductDetailDto? Product
        {
            get => _product;
            private set => SetField(ref _product, value);
        }

        public ICommand GoBackCommand { get; }

        public ProductDetailViewModel(
            IProductService productService,
            INavigationService navigation)
        {
            _productService = productService;
            _navigation = navigation;

            GoBackCommand = new RelayCommand(
                () => _navigation.GoToWarehouseDetail(_warehouseId));
        }

        public void LoadData(int productId)
        {
            Product = _productService.GetProductDetail(productId);
            _warehouseId = Product?.WarehouseId ?? 0;
        }
    }
}