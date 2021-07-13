using Restourant.Data.Foods;
using Restourant.Data.Sold.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restourant.Data.Sold
{
    public class FoodSold : IFoodSold
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public Food Food { get; init; }
        public string FoodId { get; init; }
        public DateTime DateSold { get; init; }
        public int SoldTimes { get; init; }
    }

}
