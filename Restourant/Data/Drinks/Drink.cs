using Restourant.Data.Drinks.Contracts;
using System;

namespace Restourant.Data.Drinks
{
    public class Drink : IDrink
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public int ServingSize { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }

    }
}
