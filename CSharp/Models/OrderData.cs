using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSharp.Models
{
    public static class OrderData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Joe Smith"},
                new Customer { Id = 2, Name = "John Snow"}
                );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, CustomerId = 1, SubTotal = 9.99M},
                new Order { Id = 2, CustomerId = 2, SubTotal = 13.99M}
                );

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Burger", Price = 9.99M, OrderId = 1 },
                new Item { Id = 2, Name = "Chips", Price = 3.00M, OrderId = 2 },
                new Item { Id = 3, Name = "Drink", Price = 1.00M, OrderId = 2 },
                new Item { Id = 4, Name = "Burger", Price = 9.99M, OrderId = 2 }
                );


        }
    }
}
