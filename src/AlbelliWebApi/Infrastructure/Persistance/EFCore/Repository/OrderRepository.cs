using AlbelliWebApi.Data.Entities;
using AlbelliWebApi.Data.Repositories;

namespace AlbelliWebApi.Infrastructure.Persistance.EFCore.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AlbelliDataContext _dbContext;
        public OrderRepository(AlbelliDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool OrderIdExists(string orderId)
        {
            return _dbContext.Orders.Any(p => p.OrderId == orderId);
        }

        public int Create(string orderId)
        {

            var order = new Order
            {
                OrderId = orderId,
                CreateTime = DateTime.UtcNow
            };


            _dbContext.Orders.Add(order);
           return _dbContext.SaveChanges();
        }


        public OrderView Get(string orderId)
        {
            var orderView = new OrderView();
            var items = (from o in _dbContext.Orders
                         join oi in _dbContext.OrderItems on o.OrderId equals oi.OrderId
                         join p in _dbContext.ProductTypes on oi.ProductType equals p.Id
                         where o.OrderId == orderId
                         select new OrderItemView
                         {
                             ProductId = p.Id,
                             Id = p.Id,
                             ProductName = p.Name,
                             Quantity = oi.Quantity
                         });

            if (items.Any())
            {
                orderView.OrderId = orderId;
                orderView.Items = items.ToList();
            }
             
            return orderView;
        }

}
}
