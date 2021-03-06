using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restourant.Models
{
    public class FoodViewModel
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public string Name { get; init; }
        public int ServingSize { get; init; }
        public decimal Price { get; init; }
    }
}
