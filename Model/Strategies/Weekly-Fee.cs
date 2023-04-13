namespace BookStore.Model
{
    public class WeeklyFeeStrategy : ICalculateFeeStrategy
    {
        private const decimal WeeklyLateFee = 2.0M; // Weekly late fee for books

        public decimal CalculateFee(DateTime dutDate, DateTime returnDate)
        {
            decimal fee = 0;
            TimeSpan timeSpan = returnDate - dutDate;
            if (timeSpan.Days > 0)
            {
                fee = timeSpan.Days * WeeklyLateFee;
            }
            return fee;
        }
    }
}