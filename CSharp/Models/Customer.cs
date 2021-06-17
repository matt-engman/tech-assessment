using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
