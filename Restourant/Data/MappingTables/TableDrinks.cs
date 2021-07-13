using Restourant.Data.Drinks;
using Restourant.Data.Tables;

namespace Restourant.Data.MappingTables
{
    public class TableDrinks
    {
        public Table Table { get; init; }

        public string TableId { get; init; }

        public Drink Drink { get; init; }

        public string DrinkId { get; init; }

        public int OrderTimes { get; init; }


    }
}
