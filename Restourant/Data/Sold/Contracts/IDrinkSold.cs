using Restourant.Data.Drinks;
using System;


namespace Restourant.Data.Sold.Contracts
{
    public interface IDrinkSold
    {
        string Id { get; init; }
        Drink Drink { get;  }
        string DrinkId { get;  }

        DateTime DateSold { get;  }

        int SoldTime { get;  }

    }
}
