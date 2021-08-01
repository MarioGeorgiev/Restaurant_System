using Restourant.Data.Drinks;
using Restourant.Data.Tables;

namespace Restourant.Data.MappingTables
{
    public class TableDrinks
    {
        public Table Table { get; set; }

        public int TableId { get; set; }

        public Drink Drink { get; set; }

        public string DrinkId { get; set; }

        public int OrderTimes { get; set; }


    }
}
