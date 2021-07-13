using Restourant.Data.Drinks.Contracts;
using Restourant.Data.MappingTables;
using Restourant.Data.Models.Sold;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restourant.Data.Drinks
{
    public class Drink : IDrink
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public string Name { get; init; }

        public int ServingSize { get; init; }

        public decimal Price { get; init; }

        public string Brand { get; init; }

    }
}
