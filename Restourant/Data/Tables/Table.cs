using Microsoft.AspNetCore.Identity;
using Restourant.Data.MappingTables;
using Restourant.Data.Tables.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restourant.Data.Tables
{
    public class Table : ITable
    {
        public int Id { get; init; }
        public int Capacity { get; set; }
        public int NumberOfPeople { get; set; }
        public bool IsReserved { get; set; }
        public decimal Bill { get; set; }
		
		public ICollection<TableFoods> FoodOrders { get; set; }
        public ICollection<TableDrinks> DrinkOrders { get; set; }

    }


}
