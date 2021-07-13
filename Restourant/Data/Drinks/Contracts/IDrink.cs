namespace Restourant.Data.Drinks.Contracts
{
    public interface IDrink
    {
        string Id { get; init; }
        string Name { get; }
        int ServingSize { get; }
        decimal Price { get; }
        string Brand { get; }
    }
}
