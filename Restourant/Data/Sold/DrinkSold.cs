using Restourant.Data.Drinks;
using Restourant.Data.Sold.Contracts;
using System;


namespace Restourant.Data.Models.Sold
{
    public class DrinkSold : IDrinkSold
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public Drink Drink { get ; init; }
        public string DrinkId { get; init; }       
        public DateTime DateSold { get ; init; }
        public int SoldTime { get ; init; }
    }
}
