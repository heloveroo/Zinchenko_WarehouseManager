using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WarehouseManager.Repositories;
using WarehouseManager.Repositories.Interfaces;
using WarehouseManager.Services;
using WarehouseManager.Services.Interfaces;
using WarehouseManager.ViewModels;
using WarehouseManager.ViewModels.Navigation;
using WarehouseManager.WpfApp.Services;

namespace WarehouseManager.WpfApp
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection services = new();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();

            // Стартова сторінка
            ServiceProvider.GetRequiredService<INavigationService>().GoToWarehouseList();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // Repositories
            services.AddSingleton<IWarehouseRepository, WarehouseRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();

            // Services
            services.AddSingleton<IWarehouseService, WarehouseService>();
            services.AddSingleton<IProductService, ProductService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<WarehousesViewModel>();
            services.AddTransient<WarehouseDetailViewModel>();
            services.AddTransient<ProductDetailViewModel>();

            // Navigation (Singleton бо зберігає MainViewModel)
            services.AddSingleton<INavigationService, NavigationService>();

            // Window
            services.AddSingleton<MainWindow>();
        }
    }
}