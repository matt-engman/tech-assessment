using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSharp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public int OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
