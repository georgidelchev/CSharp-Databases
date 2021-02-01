namespace PetStore.Services.Interfaces
{
    public interface IOrderService
    {
        void CompleteOrder(int orderId);
    }
}