using Restourant.Models.Api.Drinks;
using Restourant.Models.Api.Foods;
using System.Collections.Generic;

namespace Restourant.Models.Api.TableOrders
{

    public class TableOrdersApiModel
    {
        public IEnumerable<DrinksApiModel> DrinksOrdered { get; set; }

        public IEnumerable<FoodsApiModel> FoodsOrdered { get; set; }

        public int DrinksOrderedCount { get; set; }
        public int FoodsOrderedCount { get; set; }
        public decimal Bill { get; set; }
    }
}
