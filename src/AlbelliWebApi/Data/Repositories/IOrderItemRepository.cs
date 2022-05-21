using AlbelliWebApi.Data.Entities;

namespace AlbelliWebApi.Data.Repositories
{
    public interface IOrderItemRepository
    {
        void CreateItems(List<OrderItem> orderItems);
    }
}
