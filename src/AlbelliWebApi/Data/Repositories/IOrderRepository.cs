using AlbelliWebApi.Data.Entities;

namespace AlbelliWebApi.Data.Repositories
{
    public interface IOrderRepository
    {
        int Create(string orderId);
        OrderView Get(string orderId);
        bool OrderIdExists(string orderId);
    }
}
