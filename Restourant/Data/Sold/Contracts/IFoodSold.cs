using Restourant.Data.Foods;
using Restourant.Data.User;
using System;

namespace Restourant.Data.Sold.Contracts
{
    public interface IFoodSold
    {
        string Id { get; init; }
        Food Food { get; }
        string FoodId { get; }
        DateTime DateSold { get; set; }
        int SoldTimes { get; set; }

        ApplicationUser ApplicationUser { get; }

        string ApplicationUserId { get; }

    }
}
