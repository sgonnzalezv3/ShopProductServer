using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProductServer.Model
{
    public class BlazorBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Exhausted { get; set; }
        public List<TypeBook> TypeBook { get; set; }
    }
}
