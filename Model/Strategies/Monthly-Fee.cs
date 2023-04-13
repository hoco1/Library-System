namespace BookStore.Model
{
    public class MonthlyFeeStrategy : ICalculateFeeStrategy
    {
        private const decimal MonthlyLateFee = 5.0M; // Monthly late fee for books

        public decimal CalculateFee(DateTime dutDate, DateTime returnDate)
        {
            decimal fee = 0;
            TimeSpan timeSpan = returnDate - dutDate;
            if (timeSpan.Days > 0)
            {
                fee = timeSpan.Days * MonthlyLateFee;
            }
            return fee;
        }
    }
}