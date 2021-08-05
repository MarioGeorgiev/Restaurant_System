using Restourant.Models.Api.Drinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restourant.Models.Api.TableOrders
{
    public class DrinksOnTableApiModel : DrinksApiModel
    {
        public int OrderTimes { get; set; }
    }
}
