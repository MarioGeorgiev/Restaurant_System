using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restourant.Models.Api.Foods
{
    public class FoodsApiModel
    {
        public string Name { get; init; }
        public int ServingSize { get; init; }
        public decimal Price { get; init; }
    }
}
