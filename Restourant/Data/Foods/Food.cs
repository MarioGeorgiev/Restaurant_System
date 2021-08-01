using Restourant.Data.Foods.Contracts;
using Restourant.Data.MappingTables;
using Restourant.Data.Sold;
using System;
using System.Collections.Generic;

namespace Restourant.Data.Foods
{
    public class Food : IFood
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int ServingSize { get; set; }
        public decimal Price { get; set; }
		
		public ICollection<FoodSold> FoodsSold { get; set; }

        
        public ICollection<TableFoods> TableFoods { get; set; }
    }
}
