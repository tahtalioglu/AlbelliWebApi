using AlbelliWebApi.Data.Entities;
using AlbelliWebApi.Data.Repositories;
using AlbelliWebApi.Models.Responses;
using AlbelliWebApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using AlbelliWebApi.Infrastructure.Constant;

namespace AlbelliWebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
        }

         
        public BaseResponse<string> Create(OrderRequest request)
        {
            var response = new BaseResponse<string>();
            bool isExist = _orderRepository.OrderIdExists(request.OrderId);

            if (isExist)
            {
                response.Errors.Add(new Infrastructure.ExceptionHandling.Error() { ErrorMessage ="OrderId Allready Exists"});
                return response;
            }

            _orderRepository.Create(request.OrderId);

            var orderItems = request.Items.Select(x => new OrderItem()
            {
                OrderId = request.OrderId,
                ProductType = (int)x.ProductType,
                Quantity = x.Quantity,
            }).ToList();

            _orderItemRepository.CreateItems(orderItems);

            response.Result = request.OrderId;
 
            return response;
        }

        public BaseResponse<OrderResponse> Get(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new Exception(ErrorCodeConstants.OrderIdRequired);
            }
            var response = new BaseResponse<OrderResponse>();

            var productTypes = _productRepository.GetAll();

            var order = _orderRepository.Get(orderId);

            if (order == null)
            {
                return response;
            }

            var orderResponse = new OrderResponse()
            {
                OrderID = order.OrderId,
                RequiredBinWidth = CalculateOrderBinWidth(order.Items, productTypes),
                Items = order.Items.Select(p => new OrderItemResponse()
                {
                    Quantity = p.Quantity,
                   ProductType =p.ProductName,
                }).ToList(),
            };

            response.Result = orderResponse;
            return response;
        }

        public double CalculateOrderBinWidth(List<OrderItemView> orderItems, List<ProductType> productTypes)
        {
            var binWidth = default(double);

            foreach (var item in orderItems)
            {
                double productBinWidth = CalculeteItemBinWidth(productTypes, item);
                binWidth += productBinWidth;
            }

            return binWidth;
        }

        private static double CalculeteItemBinWidth(List<ProductType> productTypes, OrderItemView item)
        {
            double productBinWidth = default;

            var productType = productTypes.FirstOrDefault(x => x.Id == item.ProductId);
            if (productType != null)
            {
                var numberOfBin = (item.Quantity / productType.StackUpLimit) + ((item.Quantity % productType.StackUpLimit) > 0 ? 1 : 0);
                productBinWidth = numberOfBin * productType.PackageWidth;
            }

            return productBinWidth;
        }
    }
}
