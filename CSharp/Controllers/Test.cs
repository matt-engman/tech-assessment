using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CSharp.Models;

namespace CSharp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class Test : ControllerBase
	{
		private readonly OrderDB _orderDB;

		public Test(OrderDB orderDB)
        {
			_orderDB = orderDB;
			_orderDB.Database.EnsureCreated();
        }

		//Display on Page Load to choose customer
		[HttpGet]
		public List<Customer> GetAllCustomers()
		{
			var customers = _orderDB.Customers;
			foreach (var c in customers)
            {
				var cOrders = _orderDB.Orders.Where(o => o.CustomerId == c.Id).ToList();
				c.Orders = cOrders;
            }
			var orders = _orderDB.Orders;
			foreach (var o in orders)
			{
				var items = _orderDB.Items.Where(i => i.OrderId == o.Id).ToList();
				o.Items = items;
			}
			return customers.ToList();
		}

		//Get all orders by customer chosen
		[HttpGet("{id}")]
		public ActionResult<List<Order>> GetOrders(int id)
		{
			var orders = _orderDB.Orders.Where(o => o.CustomerId == id);
			if (orders.Count() != 0)
			{
				foreach (var order in orders)
				{
					var items = _orderDB.Items.Where(i => i.OrderId == order.Id).ToList();
					if (items != null)
					{
						order.Items = items;
					}
				}
				return Ok(orders.ToList());
			}
			return NotFound();
		}

		//Create order for customer
		[HttpPost()]
		public ActionResult NewOrder([FromBody] Order order)
		{
			if (!_orderDB.Orders.Any(o => o.Id == order.Id))
			{
				_orderDB.Orders.Add(order);
				return CreatedAtAction("Get", new { id = order.Id });
			}
			return NotFound();
		}

		//Update Order
		[HttpPut()]
		public ActionResult UpdateOrder([FromBody] Order order)
		{
			if (_orderDB.Orders.Any(o => o.Id == order.Id))
			{
				_orderDB.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				_orderDB.SaveChanges();
				return Ok();
			}
			return NotFound();
		}

		//Delete Order
		[HttpDelete("{id}")]
		public ActionResult DeleteOrder(int id)
        {
			var order = _orderDB.Orders.Find(id);
			if (order != null)
			{
				_orderDB.Orders.Remove(order);
				_orderDB.SaveChanges();
				return Ok();
			}
			return NotFound();
		}
	}
}
