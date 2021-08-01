using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restourant.Models.Api.Drinks
{
    public class DrinksApiModel
    {
        public string Id { get; init; }
        public string Name { get; init; }

        public int ServingSize { get; init; }

        public decimal Price { get; init; }

        public string Brand { get; init; }
    }
}
