using Restourant.Data.Drinks;
using Restourant.Data.Sold.Contracts;
using Restourant.Data.User;
using System;


namespace Restourant.Data.Models.Sold
{
    public class DrinkSold : IDrinkSold
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public Drink Drink { get ; init; }
        public string DrinkId { get; init; }       
        public DateTime DateSold { get ; set; }
        public int SoldTime { get ; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
