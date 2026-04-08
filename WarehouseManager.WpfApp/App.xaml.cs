using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
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

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection services = new();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            // Ініціалізація БД: створення + заповнення при першому запуску
            var context = ServiceProvider.GetRequiredService<AppDbContext>();
            await DbInitializer.InitializeAsync(context);

            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();

            ServiceProvider.GetRequiredService<INavigationService>().GoToWarehouseList();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // БД
            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WarehouseManager", "warehouse.db");
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            // Repositories
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Services
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IProductService, ProductService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<WarehousesViewModel>();
            services.AddTransient<WarehouseDetailViewModel>();
            services.AddTransient<ProductDetailViewModel>();

            // Navigation
            services.AddSingleton<INavigationService, NavigationService>();

            // Window
            services.AddSingleton<MainWindow>();
        }
    }
}