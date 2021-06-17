using CSharp.Controllers;
using CSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CSharpTests.Controllers
{
    public class TestTests : TestController
    {
        public TestTests() : base(new DbContextOptionsBuilder<OrderDB>().UseInMemoryDatabase("OrderTests").Options)
        {

        }

        [Fact]
        public void ListOrderByCustomerTest()
        {
            using (var context = new OrderDB(Options))
            {
                var controller = new Test(context);
                var result = controller.GetOrders(1).Result as OkObjectResult;
                var orders = Assert.IsType<List<Order>>(result.Value);
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public void ListOrderByCustomerNotFoundTest()
        {
            using (var context = new OrderDB(Options))
            {
                var controller = new Test(context);
                var result = controller.GetOrders(13);
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }

        [Fact]
        public void CreateValidOrderTest()
        {
            using (var context = new OrderDB(Options))
            {
                var controller = new Test(context);
                var testOrder = new Order
                {
                    Id = 3,
                    CustomerId = 1,
                    SubTotal = 10.99M,
                    Items = new List<Item>
                    {
                        new Item { Id = 1, Name = "Burger", Price = 9.99M, OrderId = 3 },
                        new Item { Id = 2, Name = "Drink", Price = 1.00M, OrderId = 3 }
                    }
                };
                var result = controller.NewOrder(testOrder);

                Assert.IsType<CreatedAtActionResult>(result);
            }
        }

        [Fact]
        public void CreateInValidOrderTest()
        {
            using (var context = new OrderDB(Options))
            {
                var controller = new Test(context);
                var testOrder = new Order
                {
                    Id = 1,
                    CustomerId = 3,
                    SubTotal = 1.00M,
                    Items = new List<Item>
                    {
                        new Item { Id = 1, Name = "Drink", Price = 1.00M, OrderId = 3 }
                    }
                };
                var result = controller.NewOrder(testOrder);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public void UpdateOrderTest()
        {
            using (var context = new OrderDB(Options))
            {
                var controller = new Test(context);
                var testOrder = new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    SubTotal = 10.99M,
                    Items = new List<Item>
                    {
                        new Item { Id = 1, Name = "Burger", Price = 9.99M, OrderId = 1 },
                        new Item { Id = 5, Name = "Drink", Price = 1.00M, OrderId = 1 }
                    }
                };
                var result = controller.UpdateOrder(testOrder);
                Assert.IsType<OkResult>(result);
            }
        }

        [Fact]
        public void UpdateOrderNotFoundTest()
        {
            using (var context = new OrderDB(Options))
            {
                var controller = new Test(context);
                var testOrder = new Order
                {
                    Id = 13,
                    CustomerId = 1,
                    SubTotal = 9.99M,
                    Items = new List<Item>
                {
                    new Item { Id = 1, Name = "Burger", Price = 9.99M, OrderId = 1 }
                }
                };
                var result = controller.UpdateOrder(testOrder);
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public void DeleteOrderTest()
        {
            using (var context = new OrderDB(Options))
            {
                var controller = new Test(context);
                var result = controller.DeleteOrder(2) as OkObjectResult;
                var removed = controller.GetOrders(2);
                Assert.IsType<NotFoundResult>(removed.Result);
            }
        }

        [Fact]
        public void DeleteOrderNotFoundTest()
        {
            using (var context = new OrderDB(Options))
            {
                var controller = new Test(context);
                var result = controller.DeleteOrder(13);
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
