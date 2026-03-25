namespace WarehouseManager.ViewModels.Navigation
{
    /// <summary>Контракт навігаційного сервісу.</summary>
    public interface INavigationService
    {
        void GoToWarehouseList();
        void GoToWarehouseDetail(int warehouseId);
        void GoToProductDetail(int productId);
    }
}