namespace AlbelliWebApi.Data.Entities
{
    public class OrderView
    {
        public string OrderId { get; set; }
        public List<OrderItemView> Items { get; set; }
    }
}
