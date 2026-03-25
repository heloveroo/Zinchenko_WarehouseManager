using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WarehouseManager.ViewModels
{
    /// <summary>Базовий клас для всіх ViewModel-ів. Реалізує INotifyPropertyChanged.</summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}