namespace AlbelliWebApi.Models.Requests
{
    public class OrderRequest
    {
        public string OrderId { get; set; }
        public List<OrderItemRequest> Items { get; set; }
        public OrderRequest() => Items = new List<OrderItemRequest>();
    }
}
