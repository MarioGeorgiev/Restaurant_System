using System.Collections.Generic;

namespace Restourant.Models.Api.TableOrders
{

    public class TableOrdersApiModel
    {
        public IEnumerable<DrinksOnTableApiModel> DrinksOrdered { get; set; }

        public IEnumerable<FoodsOnTableApiModel> FoodsOrdered { get; set; }

        public decimal Bill { get; set; }
    }
}
