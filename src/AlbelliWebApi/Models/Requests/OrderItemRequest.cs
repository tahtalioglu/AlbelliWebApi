using AlbelliWebApi.Infrastructure.Enums;

namespace AlbelliWebApi.Models.Requests
{
    public class OrderItemRequest
    {
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
    }
}
