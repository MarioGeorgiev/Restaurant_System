using Restourant.Data.Foods;
using System;

namespace Restourant.Data.Sold.Contracts
{
    public interface IFoodSold
    {
        string Id { get; init; }
        Food Food { get; }
        string FoodId { get; }
        DateTime DateSold { get; }
        int SoldTimes { get; }

    }
}
