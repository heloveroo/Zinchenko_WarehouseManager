using System.Collections.ObjectModel;
using System.Windows.Input;
using WarehouseManager.Services.DTOs;
using WarehouseManager.Services.Interfaces;
using WarehouseManager.ViewModels.Navigation;


namespace WarehouseManager.ViewModels
{
    /// <summary>ViewModel сторінки зі списком складів.</summary>
    public class WarehousesViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;
        private readonly INavigationService _navigation;

        public ObservableCollection<WarehouseListDto> Warehouses { get; } = new();
        public ICommand SelectWarehouseCommand { get; }

        public WarehousesViewModel(
            IWarehouseService warehouseService,
            INavigationService navigation)
        {
            _warehouseService = warehouseService;
            _navigation = navigation;

            SelectWarehouseCommand = new RelayCommand<int>(
                id => _navigation.GoToWarehouseDetail(id));

            LoadData();
        }

        private void LoadData()
        {
            Warehouses.Clear();
            foreach (WarehouseListDto dto in _warehouseService.GetAllWarehouses())
                Warehouses.Add(dto);
        }
    }
}