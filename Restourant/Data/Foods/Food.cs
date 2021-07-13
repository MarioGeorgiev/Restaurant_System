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
        public string Name { get; init; }
        public int ServingSize { get; init; }
        public decimal Price { get; init; }
    }
}
