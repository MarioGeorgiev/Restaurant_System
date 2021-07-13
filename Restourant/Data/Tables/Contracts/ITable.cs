
namespace Restourant.Data.Tables.Contracts
{
    public interface ITable
    {
        string Id { get; init; }
        int Capacity { get; }
        int NumberOfPeople { get;  }
        bool IsReserved { get;  }
        decimal Bill { get;  }

    }
}
