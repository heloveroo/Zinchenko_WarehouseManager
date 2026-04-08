namespace WarehouseManager.ViewModels.Navigation
{
    public interface INavigationService
    {
        void GoToWarehouseList();
        void GoToWarehouseDetail(int warehouseId);
        void GoToNewWarehouse();
        void GoToProductDetail(int productId);
        void GoToNewProduct(int warehouseId);
    }
}