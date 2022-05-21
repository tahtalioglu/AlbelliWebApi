using System;
using System.Collections.Generic;
using AlbelliWebApi.Data.Entities;
using AlbelliWebApi.Data.Repositories;
using AlbelliWebApi.Models.Requests;
using AlbelliWebApi.Services;
using AutoFixture;
using Moq;
using Xunit;
namespace AlbelliWebApi.Tests
{
    public class OrderServiceTests :TestBase
    {


        private readonly Mock<IOrderItemRepository> _orderItemRepository;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IProductRepository> _productRepository;
        private IOrderService _orderService;
        private readonly List<Data.Entities.ProductType> productTypes;
        public OrderServiceTests()
        {
            _orderItemRepository = MockFor<IOrderItemRepository>();
            _orderRepository = MockFor<IOrderRepository>();
            _productRepository = MockFor<IProductRepository>();

            productTypes = new List<Data.Entities.ProductType>()
            {
              new Data.Entities.ProductType()
                {
                    Id = 1,
                    Name = "PhotoBook",
                    PackageWidth = 19,
                    StackUpLimit = 1
                },
                new Data.Entities.ProductType()
            {
                Id = 2,
                Name = "Calendar",
                PackageWidth = 10,
                StackUpLimit = 1
            },
                new Data.Entities.ProductType()
            {
                Id = 3,
                Name = "Canvas",
                PackageWidth = 16,
                StackUpLimit = 1
            },
                new Data.Entities.ProductType()
            {
                Id = 4,
                Name = "Cards",
                PackageWidth = 4.7,
                StackUpLimit = 1
            },
                new Data.Entities.ProductType()
            {
                Id = 5,
                Name = "Mug",
                PackageWidth = 94,
                StackUpLimit = 4
            }
        };

        }
        [Fact]
        public void Should_Return_Only_One_Mug_Width_When_Four_Mug_Order_Get_Is_Called()
        {
            var orderView = new OrderView()
            {
                OrderId = "abc",
                Items = new List<OrderItemView>()
                {
                    new OrderItemView()
                    {
                        ProductId=5,
                        Quantity =4,
                        ProductName = "Mug"
                    }
                }
            };

            _productRepository.Setup(x => x.GetAll()).Returns(productTypes);

            _orderRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(orderView);

            //Arrange

            _orderService = new OrderService(_orderRepository.Object, _productRepository.Object, _orderItemRepository.Object);

            var response = _orderService.Get("abc");

            //Assert

            Assert.Equal(94, response.Result.RequiredBinWidth);
        }
        [Fact]
        public void Should_Return_Two_Mug_Width_When_Five_Mug_Order_Get_Is_Called()
        {
            //Arrange

            var orderView = new OrderView()
            {
                OrderId = "def",
                Items = new List<OrderItemView>()
                {
                    new OrderItemView()
                    {
                        ProductId=5,
                        Quantity =5,
                        ProductName = "Mug"
                    }
                }
            };

            _productRepository.Setup(x => x.GetAll()).Returns(productTypes);

            _orderRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(orderView);

            //Act

            _orderService = new OrderService(_orderRepository.Object, _productRepository.Object, _orderItemRepository.Object);

            var response = _orderService.Get("def");

            //Assert

            Assert.Equal(188, response.Result.RequiredBinWidth);
        }

        [Fact]
        public void Should_Return_Canvas_Width_When_One_Canvas_Order_Get_Is_Called()
        {
            //Arrange

            var orderView = new OrderView()
            {
                OrderId = "ghi",
                Items = new List<OrderItemView>()
                {
                    new OrderItemView()
                    {
                        ProductId=3,
                        Quantity =1,
                        ProductName = "Canvas"
                    }
                }
            };

            _productRepository.Setup(x => x.GetAll()).Returns(productTypes);

            _orderRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(orderView);

            //Act

            _orderService = new OrderService(_orderRepository.Object, _productRepository.Object, _orderItemRepository.Object);

            var response = _orderService.Get("ghi");

            //Assert

            Assert.Equal(16, response.Result.RequiredBinWidth);
        }

        [Fact]
        public void Should_Return_Exception_When_OrderId_Empty_Get_Is_Called()
        {
           
            //Act

            _orderService = new OrderService(_orderRepository.Object, _productRepository.Object, _orderItemRepository.Object);

            Assert.Throws<Exception>(() => _orderService.Get(""));
        }
        [Fact]
        public void Should_Return_Null_When_OrderId_Exists_Post_Is_Called()
        {
            //Arrange

            string orderId = "abc";

            OrderRequest orderRequest = FixtureRepository.Create<OrderRequest>();
            orderRequest.OrderId = orderId;

            _orderRepository.Setup(x => x.OrderIdExists(It.IsAny<string>())).Returns(true);

            //Act

           
            _orderService = new OrderService(_orderRepository.Object, _productRepository.Object, _orderItemRepository.Object);
            var response = _orderService.Create(orderRequest);

            //Assert

            Assert.Equal(null, response.Result);
        }
        [Fact]
        public void Should_Return_OrderId_When_OrderId_Not_Exists_Post_Is_Called()
        {
            //Arrange

            string orderId = "abc";

            OrderRequest orderRequest = FixtureRepository.Create<OrderRequest>();
            orderRequest.OrderId = orderId;

           

            _orderRepository.Setup(x => x.OrderIdExists(It.IsAny<string>())).Returns(false);
            _orderRepository.Setup(x => x.Create(It.IsAny<string>())).Returns(1);
            _orderItemRepository.Setup(x => x.CreateItems(It.IsAny<List<OrderItem>>()));
            //Act


            _orderService = new OrderService(_orderRepository.Object, _productRepository.Object, _orderItemRepository.Object);
            var response = _orderService.Create(orderRequest);

            //Assert

            Assert.Equal(orderId, response.Result);
        }

    }
}