﻿namespace AlbelliWebApi.Data.Entities
{
    public class OrderItemView
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}
