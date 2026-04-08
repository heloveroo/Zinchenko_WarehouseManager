using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using WarehouseManager.Models.Enums;
using WarehouseManager.Services.DTOs;
using WarehouseManager.Services.Interfaces;
using WarehouseManager.ViewModels.Navigation;

namespace WarehouseManager.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly INavigationService _navigation;

        private ProductDetailDto? _product;
        private bool _isLoading;
        private bool _isEditing;
        private bool _isNewProduct;
        private int _currentProductId;
        private int _currentWarehouseId;

        // ---- Поля редагування ----
        private string _editName = string.Empty;
        private int _editQuantity;
        private decimal _editUnitPrice;
        private ProductCategory _editCategory;
        private string _editDescription = string.Empty;

        // ---- Публічні властивості ----
        public ProductDetailDto? Product
        {
            get => _product;
            private set => SetField(ref _product, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetField(ref _isLoading, value);
        }

        public bool IsEditing
        {
            get => _isEditing;
            set => SetField(ref _isEditing, value);
        }

        public string EditName
        {
            get => _editName;
            set => SetField(ref _editName, value);
        }

        public int EditQuantity
        {
            get => _editQuantity;
            set { SetField(ref _editQuantity, value); OnPropertyChanged(nameof(EditTotalPrice)); }
        }

        public decimal EditUnitPrice
        {
            get => _editUnitPrice;
            set { SetField(ref _editUnitPrice, value); OnPropertyChanged(nameof(EditTotalPrice)); }
        }

        public ProductCategory EditCategory
        {
            get => _editCategory;
            set => SetField(ref _editCategory, value);
        }

        public string EditDescription
        {
            get => _editDescription;
            set => SetField(ref _editDescription, value);
        }

        public decimal EditTotalPrice => EditQuantity * EditUnitPrice;

        public IEnumerable<ProductCategory> Categories =>
            Enum.GetValues<ProductCategory>();

        // ---- Команди ----
        public ICommand GoBackCommand { get; }
        public ICommand EditProductCommand { get; }
        public AsyncRelayCommand SaveCommand { get; }
        public ICommand CancelEditCommand { get; }

        public ProductDetailViewModel(
            IProductService productService,
            INavigationService navigation)
        {
            _productService = productService;
            _navigation = navigation;

            GoBackCommand = new RelayCommand(GoBack);
            EditProductCommand = new RelayCommand(StartEdit, () => !IsEditing);
            SaveCommand = new AsyncRelayCommand(SaveAsync);
            CancelEditCommand = new RelayCommand(CancelEdit);
        }

        // ---- Завантаження існуючого товару ----
        public async Task LoadDataAsync(int productId)
        {
            _isNewProduct = false;
            _currentProductId = productId;
            IsEditing = false;
            IsLoading = true;
            try
            {
                Product = await _productService.GetProductDetailAsync(productId);
                if (Product is not null)
                    _currentWarehouseId = Product.WarehouseId;
            }
            finally { IsLoading = false; }
        }

        // ---- Новий товар ----
        public void StartNewProduct(int warehouseId)
        {
            _isNewProduct = true;
            _currentWarehouseId = warehouseId;
            _currentProductId = 0;
            Product = null;
            EditName = string.Empty;
            EditQuantity = 0;
            EditUnitPrice = 0;
            EditCategory = ProductCategory.Other;
            EditDescription = string.Empty;
            IsEditing = true;
        }

        // ---- Редагування ----
        private void StartEdit()
        {
            if (Product is null) return;
            EditName = Product.Name;
            EditQuantity = Product.Quantity;
            EditUnitPrice = Product.UnitPrice;
            Enum.TryParse<ProductCategory>(Product.Category, out var cat);
            EditCategory = cat;
            EditDescription = Product.Description;
            IsEditing = true;
        }

        private async Task SaveAsync()
        {
            IsLoading = true;
            try
            {
                if (_isNewProduct)
                {
                    await _productService.AddProductAsync(
                        _currentWarehouseId, EditName, EditQuantity,
                        EditUnitPrice, EditCategory, EditDescription);
                }
                else
                {
                    await _productService.UpdateProductAsync(
                        _currentProductId, EditName, EditQuantity,
                        EditUnitPrice, EditCategory, EditDescription);
                }
                _navigation.GoToWarehouseDetail(_currentWarehouseId);
            }
            finally { IsLoading = false; }
        }

        private void CancelEdit()
        {
            if (_isNewProduct)
                _navigation.GoToWarehouseDetail(_currentWarehouseId);
            else
                IsEditing = false;
        }

        private void GoBack() =>
            _navigation.GoToWarehouseDetail(_currentWarehouseId);
    }
}