namespace WarehouseManager.ViewModels
{
    /// <summary>
    /// Кореневий ViewModel головного вікна.
    /// Зберігає поточну сторінку — ContentControl у MainWindow відображає її через DataTemplate.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel = null!;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetField(ref _currentViewModel, value);
        }
    }
}