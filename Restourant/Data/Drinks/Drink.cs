using Restourant.Data.Drinks.Contracts;
using Restourant.Data.MappingTables;
using Restourant.Data.Models.Sold;
using System;
using System.Collections.Generic;

namespace Restourant.Data.Drinks
{
    public class Drink : IDrink
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public int ServingSize { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }
		
		public ICollection<DrinkSold> DrinkssSold { get; set; }


        public ICollection<TableDrinks> TableDrinks { get; set; }

    }
}
