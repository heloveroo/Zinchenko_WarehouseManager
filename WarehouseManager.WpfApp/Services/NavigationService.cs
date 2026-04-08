using Microsoft.Extensions.DependencyInjection;
using WarehouseManager.ViewModels;
using WarehouseManager.ViewModels.Navigation;

namespace WarehouseManager.WpfApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly MainViewModel _mainViewModel;
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(MainViewModel mainViewModel, IServiceProvider serviceProvider)
        {
            _mainViewModel = mainViewModel;
            _serviceProvider = serviceProvider;
        }

        public void GoToWarehouseList()
        {
            var vm = _serviceProvider.GetRequiredService<WarehousesViewModel>();
            _mainViewModel.CurrentViewModel = vm;
            _ = vm.LoadDataAsync();
        }

        public void GoToWarehouseDetail(int warehouseId)
        {
            var vm = _serviceProvider.GetRequiredService<WarehouseDetailViewModel>();
            _mainViewModel.CurrentViewModel = vm;
            _ = vm.LoadDataAsync(warehouseId);
        }

        public void GoToNewWarehouse()
        {
            var vm = _serviceProvider.GetRequiredService<WarehouseDetailViewModel>();
            vm.StartNewWarehouse();
            _mainViewModel.CurrentViewModel = vm;
        }

        public void GoToProductDetail(int productId)
        {
            var vm = _serviceProvider.GetRequiredService<ProductDetailViewModel>();
            _mainViewModel.CurrentViewModel = vm;
            _ = vm.LoadDataAsync(productId);
        }

        public void GoToNewProduct(int warehouseId)
        {
            var vm = _serviceProvider.GetRequiredService<ProductDetailViewModel>();
            vm.StartNewProduct(warehouseId);
            _mainViewModel.CurrentViewModel = vm;
        }
    }
}