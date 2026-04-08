using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WarehouseManager.Services.DTOs;
using WarehouseManager.Services.Interfaces;
using WarehouseManager.ViewModels.Navigation;

namespace WarehouseManager.ViewModels
{
    public class WarehousesViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;
        private readonly INavigationService _navigation;

        private readonly List<WarehouseListDto> _allWarehouses = new();
        private string _searchText = string.Empty;
        private string _selectedSort = "Назва";
        private bool _sortDescending;
        private bool _isLoading;

        public ObservableCollection<WarehouseListDto> Warehouses { get; } = new();

        public string SearchText
        {
            get => _searchText;
            set { SetField(ref _searchText, value); RefreshDisplay(); }
        }

        public string SelectedSort
        {
            get => _selectedSort;
            set { SetField(ref _selectedSort, value); RefreshDisplay(); }
        }

        public bool SortDescending
        {
            get => _sortDescending;
            set { SetField(ref _sortDescending, value); RefreshDisplay(); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetField(ref _isLoading, value);
        }

        public List<string> SortOptions { get; } = new()
        {
            "Назва", "Місто", "Кількість товарів"
        };

        public ICommand SelectWarehouseCommand { get; }
        public ICommand AddWarehouseCommand { get; }
        public RelayCommand<WarehouseListDto> DeleteWarehouseCommand { get; }

        public WarehousesViewModel(
            IWarehouseService warehouseService,
            INavigationService navigation)
        {
            _warehouseService = warehouseService;
            _navigation = navigation;

            SelectWarehouseCommand = new RelayCommand<int>(
                id => _navigation.GoToWarehouseDetail(id));

            AddWarehouseCommand = new RelayCommand(
                () => _navigation.GoToNewWarehouse());

            DeleteWarehouseCommand = new RelayCommand<WarehouseListDto>(
                dto => _ = DeleteWarehouseAsync(dto));
        }

        public async Task LoadDataAsync()
        {
            IsLoading = true;
            try
            {
                var list = await _warehouseService.GetAllWarehousesAsync();
                _allWarehouses.Clear();
                _allWarehouses.AddRange(list);
                RefreshDisplay();
            }
            finally { IsLoading = false; }
        }

        private async Task DeleteWarehouseAsync(WarehouseListDto? dto)
        {
            if (dto is null) return;
            IsLoading = true;
            try
            {
                await _warehouseService.DeleteWarehouseAsync(dto.Id);
                _allWarehouses.Remove(dto);
                RefreshDisplay();
            }
            finally { IsLoading = false; }
        }

        private void RefreshDisplay()
        {
            var filtered = _allWarehouses
                .Where(w => string.IsNullOrWhiteSpace(SearchText) ||
                            w.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                            w.Location.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            filtered = SelectedSort switch
            {
                "Місто" => SortDescending
                    ? filtered.OrderByDescending(w => w.Location)
                    : filtered.OrderBy(w => w.Location),
                "Кількість товарів" => SortDescending
                    ? filtered.OrderByDescending(w => w.ProductCount)
                    : filtered.OrderBy(w => w.ProductCount),
                _ => SortDescending
                    ? filtered.OrderByDescending(w => w.Name)
                    : filtered.OrderBy(w => w.Name),
            };

            Warehouses.Clear();
            foreach (var item in filtered) Warehouses.Add(item);
        }
    }
}