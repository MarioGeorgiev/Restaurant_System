

using Restourant.Data.Foods;
using Restourant.Data.Tables;

namespace Restourant.Data.MappingTables
{
    public class TableFoods
    {
        public Food Food { get; set; }

        public string FoodId { get; set; }

        public Table Table { get; set; }

        public int TableId { get; set; }

        public int OrderTimes { get; set; }
    }
}
