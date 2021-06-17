using CSharp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpTests.Controllers
{
    public class TestController
    {
        private DbContextOptionsBuilder<OrderDB> dbContextOptionsBuilder;

        protected DbContextOptions<OrderDB> Options { get; }
        public TestController(DbContextOptions<OrderDB> options)
        {
            Options = options;
            Seed();
        }

        private void Seed()
        {
            using (var orderContext = new OrderDB(Options))
            {
                orderContext.Database.EnsureCreated();
            }
        }
    }
}
