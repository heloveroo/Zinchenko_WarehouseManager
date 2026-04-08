# Менеджер складів — Лабораторні роботи 1–4

## Опис застосунку
Система обліку складів і товарів з повним CRUD, пошуком, сортуванням та збереженням даних у SQLite базі даних.

---

## Структура проєкту

```
WarehouseManager.sln
│
├── WarehouseManager.Models          ← Бібліотека: моделі даних (EF Core сутності)
│   ├── Enums/
│   │   ├── WarehouseLocation.cs     ← enum міст розташування складу
│   │   └── ProductCategory.cs      ← enum категорій товарів
│   ├── WarehouseModel.cs            ← модель складу
│   └── ProductModel.cs             ← модель товару
│
├── WarehouseManager.Repositories    ← Бібліотека: шар сховища (EF Core + SQLite)
│   ├── Interfaces/
│   │   ├── IWarehouseRepository.cs  ← інтерфейс репозиторію складів
│   │   └── IProductRepository.cs   ← інтерфейс репозиторію товарів
│   ├── Repositories/
│   │   ├── WarehouseRepository.cs  ← реалізація репозиторію складів
│   │   └── ProductRepository.cs   ← реалізація репозиторію товарів
│   ├── AppDbContext.cs             ← EF Core DbContext
│   └── DbInitializer.cs           ← заповнення БД початковими даними
│
├── WarehouseManager.Services        ← Бібліотека: шар сервісів і DTO
│   ├── DTOs/
│   │   ├── WarehouseListDto.cs     ← DTO для списку складів
│   │   ├── WarehouseDetailDto.cs   ← DTO для деталей складу
│   │   ├── ProductListDto.cs       ← DTO для списку товарів
│   │   └── ProductDetailDto.cs    ← DTO для деталей товару
│   ├── Interfaces/
│   │   ├── IWarehouseService.cs   ← інтерфейс сервісу складів
│   │   └── IProductService.cs     ← інтерфейс сервісу товарів
│   ├── WarehouseService.cs        ← реалізація сервісу складів
│   └── ProductService.cs          ← реалізація сервісу товарів
│
└── WarehouseManager.WpfApp          ← WPF-застосунок (UI шар)
    ├── Converters/
    │   └── Converters.cs           ← конвертери для локалізації enum
    ├── Pages/
    │   ├── WarehouseListPage       ← список складів + пошук + сортування
    │   ├── WarehouseDetailPage     ← деталі складу + список товарів
    │   └── ProductDetailPage       ← деталі товару
    ├── Services/
    │   └── NavigationService.cs   ← сервіс навігації між сторінками
    ├── App.xaml / App.xaml.cs     ← IoC-контейнер, ініціалізація БД
    └── MainWindow.xaml / .cs      ← єдине вікно з ContentControl
```

---

## Архітектура

Застосунок побудований за трьохшаровою архітектурою:

- **UI шар** (`WpfApp`) — взаємодіє тільки з шаром сервісів через інтерфейси
- **Шар сервісів** (`Services`) — бізнес-логіка, перетворення моделей у DTO
- **Шар сховища** (`Repositories`) — робота з БД через EF Core

### MVVM
- `BaseViewModel` — реалізує `INotifyPropertyChanged`
- `RelayCommand` / `AsyncRelayCommand` — реалізація `ICommand`
- `MainViewModel` — зберігає поточну сторінку (`CurrentViewModel`)
- `WarehousesViewModel`, `WarehouseDetailViewModel`, `ProductDetailViewModel` — логіка кожної сторінки

### DI / IoC
```
App.xaml.cs — IoC-контейнер (Microsoft.Extensions.DependencyInjection)
  ├── AddDbContext<AppDbContext>         ← SQLite
  ├── AddScoped<IWarehouseRepository>
  ├── AddScoped<IProductRepository>
  ├── AddScoped<IWarehouseService>
  ├── AddScoped<IProductService>
  ├── AddSingleton<INavigationService>
  └── AddSingleton<MainWindow>
```

---

## Функціонал

### Склади
- Перегляд списку всіх складів
- Пошук за назвою або містом
- Сортування за назвою, містом, кількістю товарів
- Додавання нового складу
- Редагування назви та міста
- Видалення складу (з каскадним видаленням товарів)

### Товари
- Перегляд списку товарів складу
- Пошук за назвою або категорією
- Сортування за назвою, категорією, кількістю, ціною, сумою
- Додавання нового товару
- Редагування всіх полів
- Видалення товару

---

## Сховище даних

- **SQLite** база даних через **Entity Framework Core**
- Файл БД: `%LocalAppData%\WarehouseManager\warehouse.db`
- При першому запуску автоматично створюється схема і заповнюється початковими даними (3 склади, 12 товарів)
- Каскадне видалення: при видаленні складу автоматично видаляються всі його товари

---

## Початкові дані

| Склад         | Місто    | Товарів |
|---------------|----------|---------|
| Центральний   | Київ     | 10      |
| Західний      | Львів    | 2       |
| Богуславський | Богуслав | 0       |

---

## Запуск

Відкрити `WarehouseManager.sln` → встановити `WarehouseManager.WpfApp` як стартовий проєкт → запустити (F5).