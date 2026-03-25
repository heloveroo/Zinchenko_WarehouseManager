using System.Collections.ObjectModel;
using System.Windows.Input;
using WarehouseManager.Services.DTOs;
using WarehouseManager.Services.Interfaces;
using WarehouseManager.ViewModels.Navigation;

namespace WarehouseManager.ViewModels
{
    /// <summary>ViewModel сторінки з деталями складу і списком товарів.</summary>
    public class WarehouseDetailViewModel : BaseViewModel
    {
        private readonly IWarehouseService _warehouseService;
        private readonly INavigationService _navigation;

        private WarehouseDetailDto? _warehouse;

        public WarehouseDetailDto? Warehouse
        {
            get => _warehouse;
            private set => SetField(ref _warehouse, value);
        }

        public ICommand SelectProductCommand { get; }
        public ICommand GoBackCommand { get; }

        public WarehouseDetailViewModel(
            IWarehouseService warehouseService,
            INavigationService navigation)
        {
            _warehouseService = warehouseService;
            _navigation = navigation;

            SelectProductCommand = new RelayCommand<int>(
                id => _navigation.GoToProductDetail(id));

            GoBackCommand = new RelayCommand(
                () => _navigation.GoToWarehouseList());
        }

        public void LoadData(int warehouseId)
        {
            Warehouse = _warehouseService.GetWarehouseDetail(warehouseId);
        }
    }
}