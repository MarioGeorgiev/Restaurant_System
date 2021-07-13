namespace Restourant.Data.Foods.Contracts
{
    public interface IFood
    {
        string Id { get; init; }
        string Name { get; }
        int ServingSize { get; }
        decimal Price { get; }
    }
}
