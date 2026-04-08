using System;
using System.Globalization;
using System.Windows.Data;
using WarehouseManager.Models.Enums;

namespace WarehouseManager.WpfApp.Converters
{
    public class LocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                "Kyiv" => "Київ",
                "Lviv" => "Львів",
                "Bohuslaw" => "Богуслав",
                "Kharkiv" => "Харків",
                "Odesa" => "Одеса",
                _ => value?.ToString() ?? string.Empty
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class CategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                "Electronics" => "Електроніка",
                "Clothing" => "Одяг",
                "Food" => "Продукти",
                "Furniture" => "Меблі",
                "Tools" => "Інструменти",
                "Other" => "Інше",
                _ => value?.ToString() ?? string.Empty
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}