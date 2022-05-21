namespace AlbelliWebApi.Models.Responses
{
    public class OrderResponse
    {
        public string OrderID { get; set; }
        public double RequiredBinWidth { get; set; }
        public List<OrderItemResponse> Items { get; set; }

        public OrderResponse()
        {
            Items = new List<OrderItemResponse>();
        }
    }
}
