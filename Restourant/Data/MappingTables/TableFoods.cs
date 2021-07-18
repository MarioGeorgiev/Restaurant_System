

using Restourant.Data.Foods;
using Restourant.Data.Tables;

namespace Restourant.Data.MappingTables
{
    public class TableFoods
    {
        public Food Food { get; init; }

        public string FoodId { get; init; }

        public Table Table { get; init; }

        public int TableId { get; init; }

        public int OrderTimes { get; init; }
    }
}
