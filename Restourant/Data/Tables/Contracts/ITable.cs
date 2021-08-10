
using Restourant.Data.User;

namespace Restourant.Data.Tables.Contracts
{
    public interface ITable
    {
        int Id { get; init; }
        int Capacity { get; }
        int NumberOfPeople { get;  }
        bool IsReserved { get; set; }
        decimal Bill { get; set; }

        ApplicationUser ApplicationUser { get;  }

        string ApplicationUserId { get;  }
    }
}
