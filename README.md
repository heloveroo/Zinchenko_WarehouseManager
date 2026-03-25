# Менеджер складів — Лабораторні роботи 1–2

## Опис застосунку
Система обліку складів і товарів.  
Реалізована у двох варіантах: консольний застосунок (ЛР1) та WPF-застосунок із DI/IoC (ЛР2).

---

## Структура проєкту

```
WarehouseManager.sln
│
├── WarehouseManager.Models          ← Бібліотека: класи зберігання даних
│   ├── Enums/
│   │   ├── WarehouseLocation.cs     ← enum міст розташування складу
│   │   └── ProductCategory.cs      ← enum категорій товарів
│   ├── WarehouseModel.cs            ← модель складу (тільки "сирі" дані)
│   └── ProductModel.cs             ← модель товару (тільки "сирі" дані)
│
├── WarehouseManager.ViewModels      ← Бібліотека: класи відображення/редагування
│   ├── WarehouseViewModel.cs        ← відображення складу + обчислювані поля + список товарів
│   └── ProductViewModel.cs         ← відображення товару + обчислюване поле TotalPrice
│
├── WarehouseManager.Services        ← Бібліотека: сервіси і штучне сховище
│   ├── Storage/
│   │   └── FakeStorage.cs          ← штучне сховище (internal, недоступне ззовні)
│   ├── IWarehouseService.cs        ← інтерфейс сервісу (для DIP та DI) [ЛР2]
│   └── WarehouseService.cs         ← реалізація сервісу (implements IWarehouseService)
│
├── WarehouseManager.ConsoleApp      ← Консольний застосунок (ЛР1)
│   └── Program.cs                  ← логіка навігації і виводу
│
└── WarehouseManager.WpfApp          ← WPF-застосунок (ЛР2)
    ├── App.xaml / App.xaml.cs      ← IoC-контейнер, реєстрація сервісів
    ├── MainWindow.xaml / .cs       ← єдине вікно з Frame для навігації
    └── Pages/
        ├── WarehouseListPage       ← Сторінка 1: список усіх складів
        ├── WarehouseDetailPage     ← Сторінка 2: деталі складу + список товарів
        └── ProductDetailPage       ← Сторінка 3: повна інформація про товар
```

---

## Логіка класів

### Models (класи зберігання)
- **WarehouseModel** — id (read-only), назва, місто. **Без** обчислюваних полів і колекцій.
- **ProductModel** — id (read-only), warehouseId, назва, кількість, ціна, категорія, опис. **Без** обчислюваних полів.

### ViewModels (класи відображення)
- **WarehouseViewModel** — містить `TotalValue` (сума всіх товарів), список `Products` (null до завантаження), методи `PrintShort()` і `PrintDetails()`.
- **ProductViewModel** — містить `TotalPrice` (ціна × кількість), методи `PrintShort()` і `PrintDetails()`.

### Services
- **FakeStorage** — `internal static` клас. Містить початкові дані: 3 склади і 12 товарів. Недоступний поза межами проєкту Services.
- **IWarehouseService** — інтерфейс сервісу. Забезпечує Dependency Inversion Principle: UI-застосунок залежить від абстракції, а не від конкретної реалізації.
- **WarehouseService** — реалізує `IWarehouseService`. Єдина точка доступу до сховища. Реалізує lazy-завантаження товарів.

---

## Принципи DI та IoC (ЛР2)

```
App.xaml.cs — IoC-контейнер (Microsoft.Extensions.DependencyInjection)
  ├── services.AddSingleton<IWarehouseService, WarehouseService>()
  └── services.AddTransient<MainWindow>()
         ↓
MainWindow(IWarehouseService)          ← Constructor Injection
  └── WarehouseListPage(IWarehouseService, Frame)
            ↓
      WarehouseDetailPage(IWarehouseService, Frame, WarehouseViewModel)
            ↓
        ProductDetailPage(Frame, ProductViewModel)
```

**Dependency Inversion Principle**: `MainWindow`, `WarehouseListPage` та `WarehouseDetailPage`
залежать від `IWarehouseService` (абстракції), а не від `WarehouseService` (конкретної реалізації).
Завдяки цьому можна замінити `FakeStorage` на реальну БД, не змінюючи жодного рядка у WpfApp.

---

## Логіка WPF-застосунку (ЛР2)

1. При запуску `App.OnStartup` збирає IoC-контейнер і відкриває `MainWindow`.
2. `MainWindow` містить єдиний `Frame` — навігація відбувається через зміну сторінок (без нових вікон).
3. **Сторінка 1 (WarehouseListPage)**: завантажує список складів, відображає картки. Клік → перехід на Сторінку 2.
4. **Сторінка 2 (WarehouseDetailPage)**: відображає деталі складу і список товарів (lazy loading). Клік на товар → Сторінка 3. Кнопка «Назад» → Сторінка 1.
5. **Сторінка 3 (ProductDetailPage)**: відображає всі поля товару. Кнопка «Назад» → Сторінка 2.

---

## Логіка консольного застосунку (ЛР1)

1. При запуску завантажується список складів (`GetAllWarehouses`), товари **не** завантажуються одразу.
2. Виводиться список складів. Користувач вводить ID складу.
3. Якщо товари ще не завантажені — відбувається завантаження (`LoadProductsForWarehouse`).
4. Виводяться деталі складу і короткий список товарів.
5. Користувач може ввести ID товару для перегляду повної інформації.
6. Введення `0` повертає до списку складів, `exit` — завершує застосунок.

---

## Початкові дані

| Склад         | Місто   | Товарів |
|---------------|---------|---------|
| Центральний   | Київ    | 10      |
| Західний      | Львів   | 2       |
| Богуславський | Богуслав| 0       |

---

## Запуск

### WPF-застосунок (ЛР2)
Відкрити `WarehouseManager.sln` → встановити `WarehouseManager.WpfApp` як стартовий проєкт → запустити (F5).

### Консольний застосунок (ЛР1)
Відкрити `WarehouseManager.sln` → встановити `WarehouseManager.ConsoleApp` як стартовий проєкт → запустити (F5).
