using AlbelliWebApi.Data.Entities;
using AlbelliWebApi.Data.Repositories;

namespace AlbelliWebApi.Infrastructure.Persistance.EFCore.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AlbelliDataContext _dbContext;
        public OrderItemRepository(AlbelliDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateItems(List<OrderItem> orderItems)
        {
          
            foreach (var item in orderItems)
            {
            
                _dbContext.OrderItems.Add(item);
                
            }
            _dbContext.SaveChanges();
            
        }
    }
}
