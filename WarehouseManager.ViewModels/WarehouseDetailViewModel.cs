using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WarehouseManager.Models.Enums;
using WarehouseManager.Services.DTOs;
using WarehouseManager.Services.Interfaces;
using WarehouseManager.ViewModels.Navigation;

namespace WarehouseManager.ViewModels
{
    public class WarehouseDetailViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;
        private readonly INavigationService _navigation;

        private WarehouseDetailDto? _warehouse;
        private readonly List<ProductListDto> _allProducts = new();

        // ---- Стан ----
        private bool _isLoading;
        private bool _isEditing;
        private bool _isNewWarehouse;
        private int _currentWarehouseId;

        // ---- Поля редагування складу ----
        private string _editName = string.Empty;
        private WarehouseLocation _editLocation;

        // ---- Пошук/сортування товарів ----
        private string _productSearch = string.Empty;
        private string _selectedSort = "Назва";
        private bool _sortDescending;
        private ProductListDto? _selectedProduct;

        // ---- Публічні властивості ----
        public WarehouseDetailDto? Warehouse
        {
            get => _warehouse;
            private set => SetField(ref _warehouse, value);
        }

        public ObservableCollection<ProductListDto> FilteredProducts { get; } = new();

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

        public WarehouseLocation EditLocation
        {
            get => _editLocation;
            set => SetField(ref _editLocation, value);
        }

        public string ProductSearch
        {
            get => _productSearch;
            set { SetField(ref _productSearch, value); RefreshProducts(); }
        }

        public string SelectedSort
        {
            get => _selectedSort;
            set { SetField(ref _selectedSort, value); RefreshProducts(); }
        }

        public bool SortDescending
        {
            get => _sortDescending;
            set { SetField(ref _sortDescending, value); RefreshProducts(); }
        }

        public ProductListDto? SelectedProduct
        {
            get => _selectedProduct;
            set => SetField(ref _selectedProduct, value);
        }

        public IEnumerable<WarehouseLocation> Locations =>
            Enum.GetValues<WarehouseLocation>();

        public List<string> SortOptions { get; } = new()
        {
            "Назва", "Категорія", "Кількість", "Ціна", "Сума"
        };

        // ---- Команди ----
        public ICommand GoBackCommand { get; }
        public ICommand SelectProductCommand { get; }
        public ICommand EditWarehouseCommand { get; }
        public AsyncRelayCommand SaveWarehouseCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand AddProductCommand { get; }
        public RelayCommand<ProductListDto> DeleteProductCommand { get; }

        public WarehouseDetailViewModel(
            IWarehouseService warehouseService,
            IProductService productService,
            INavigationService navigation)
        {
            _warehouseService = warehouseService;
            _productService = productService;
            _navigation = navigation;

            GoBackCommand = new RelayCommand(() => _navigation.GoToWarehouseList());
            SelectProductCommand = new RelayCommand<int>(id => _navigation.GoToProductDetail(id));
            EditWarehouseCommand = new RelayCommand(StartEdit, () => !IsEditing);
            SaveWarehouseCommand = new AsyncRelayCommand(SaveAsync);
            CancelEditCommand = new RelayCommand(CancelEdit);
            AddProductCommand = new RelayCommand(
                () => _navigation.GoToNewProduct(_currentWarehouseId),
                () => !_isNewWarehouse);
            DeleteProductCommand = new RelayCommand<ProductListDto>(
                dto => _ = DeleteProductAsync(dto));
        }

        // ---- Завантаження ----
        public async Task LoadDataAsync(int warehouseId)
        {
            _isNewWarehouse = false;
            _currentWarehouseId = warehouseId;
            IsEditing = false;
            IsLoading = true;
            try
            {
                Warehouse = await _warehouseService.GetWarehouseDetailAsync(warehouseId);
                _allProducts.Clear();
                if (Warehouse is not null)
                    _allProducts.AddRange(Warehouse.Products);
                RefreshProducts();
            }
            finally { IsLoading = false; }
        }

        // ---- Новий склад ----
        public void StartNewWarehouse()
        {
            _isNewWarehouse = true;
            _currentWarehouseId = 0;
            Warehouse = null;
            EditName = string.Empty;
            EditLocation = WarehouseLocation.Kyiv;
            IsEditing = true;
            _allProducts.Clear();
            FilteredProducts.Clear();
        }

        // ---- Редагування ----
        private void StartEdit()
        {
            EditName = Warehouse?.Name ?? string.Empty;
            EditLocation = Enum.TryParse<WarehouseLocation>(Warehouse?.Location, out var loc)
                ? loc : WarehouseLocation.Kyiv;
            IsEditing = true;
        }

        private async Task SaveAsync()
        {
            IsLoading = true;
            try
            {
                if (_isNewWarehouse)
                {
                    var created = await _warehouseService.AddWarehouseAsync(EditName, EditLocation);
                    _isNewWarehouse = false;
                    _currentWarehouseId = created.Id;
                    await LoadDataAsync(created.Id);
                }
                else
                {
                    await _warehouseService.UpdateWarehouseAsync(
                        _currentWarehouseId, EditName, EditLocation);
                    await LoadDataAsync(_currentWarehouseId);
                }
                IsEditing = false;
            }
            finally { IsLoading = false; }
        }

        private void CancelEdit()
        {
            if (_isNewWarehouse)
                _navigation.GoToWarehouseList();
            else
                IsEditing = false;
        }

        // ---- Видалення товару ----
        private async Task DeleteProductAsync(ProductListDto? dto)
        {
            if (dto is null) return;
            IsLoading = true;
            try
            {
                await _productService.DeleteProductAsync(dto.Id);
                _allProducts.Remove(dto);
                RefreshProducts();
                Warehouse = await _warehouseService.GetWarehouseDetailAsync(_currentWarehouseId);
            }
            finally { IsLoading = false; }
        }

        // ---- Фільтрація та сортування ----
        private void RefreshProducts()
        {
            var filtered = _allProducts
                .Where(p => string.IsNullOrWhiteSpace(ProductSearch) ||
                            p.Name.Contains(ProductSearch, StringComparison.OrdinalIgnoreCase) ||
                            p.Category.Contains(ProductSearch, StringComparison.OrdinalIgnoreCase));

            filtered = SelectedSort switch
            {
                "Категорія" => SortDescending
                    ? filtered.OrderByDescending(p => p.Category)
                    : filtered.OrderBy(p => p.Category),
                "Кількість" => SortDescending
                    ? filtered.OrderByDescending(p => p.Quantity)
                    : filtered.OrderBy(p => p.Quantity),
                "Ціна" => SortDescending
                    ? filtered.OrderByDescending(p => p.UnitPrice)
                    : filtered.OrderBy(p => p.UnitPrice),
                "Сума" => SortDescending
                    ? filtered.OrderByDescending(p => p.TotalPrice)
                    : filtered.OrderBy(p => p.TotalPrice),
                _ => SortDescending
                    ? filtered.OrderByDescending(p => p.Name)
                    : filtered.OrderBy(p => p.Name),
            };

            FilteredProducts.Clear();
            foreach (var item in filtered) FilteredProducts.Add(item);
        }
    }
}