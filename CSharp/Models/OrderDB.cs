using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSharp.Models
{
    public class OrderDB : DbContext
    {
        public OrderDB(DbContextOptions<OrderDB> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Customer>().HasMany(c => c.Orders).WithOne(o => o.Customer).HasForeignKey(o => o.CustomerId);
            mb.Entity<Order>().HasOne(o => o.Customer);
            mb.Entity<Order>().HasMany(o => o.Items).WithOne(i => i.Order).HasForeignKey(i => i.OrderId);
            mb.Entity<Item>().HasOne(i => i.Order);
            mb.Seed();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
