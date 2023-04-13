namespace BookStore.Model
{
    public class DailyFeeStrategy : ICalculateFeeStrategy
    {
        private const decimal DailyLateFee = 0.5M; // Daily late fee for books

        public decimal CalculateFee(DateTime dutDate, DateTime returnDate)
        {
            decimal fee = 0;
            TimeSpan timeSpan = returnDate - dutDate;
            
            if (timeSpan.Days > 0)
            {
                fee = timeSpan.Days * DailyLateFee;
            }
            return fee;
        }
    }
}