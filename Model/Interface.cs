namespace BookStore.Model
{
    // This is the interface for the strategy pattern
    public interface ICalculateFeeStrategy
    {
        decimal CalculateFee(DateTime dutDate, DateTime returnDate);
    }
}