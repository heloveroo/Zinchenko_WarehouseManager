using Microsoft.Extensions.DependencyInjection;
using System;
using WarehouseManager.ViewModels;
using WarehouseManager.ViewModels.Navigation;

namespace WarehouseManager.WpfApp.Services
{
    /// <summary>
    /// Реалізація навігації: змінює CurrentViewModel у MainViewModel,
    /// ContentControl автоматично підбирає потрібний DataTemplate.
    /// </summary>
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
            _mainViewModel.CurrentViewModel =
                _serviceProvider.GetRequiredService<WarehousesViewModel>();
        }

        public void GoToWarehouseDetail(int warehouseId)
        {
            WarehouseDetailViewModel vm =
                _serviceProvider.GetRequiredService<WarehouseDetailViewModel>();
            vm.LoadData(warehouseId);
            _mainViewModel.CurrentViewModel = vm;
        }

        public void GoToProductDetail(int productId)
        {
            ProductDetailViewModel vm =
                _serviceProvider.GetRequiredService<ProductDetailViewModel>();
            vm.LoadData(productId);
            _mainViewModel.CurrentViewModel = vm;
        }
    }
}