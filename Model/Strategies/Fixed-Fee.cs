namespace BookStore.Model
{
    public class FixedFeeStrategy : ICalculateFeeStrategy
    {
        private decimal _fixedFee;
        public FixedFeeStrategy(decimal feeRate)
        {
            _fixedFee = feeRate;
        }
        public decimal CalculateFee(DateTime dutDate, DateTime returnDate)
        {
            decimal fee = 0;
            TimeSpan timeSpan = returnDate - dutDate;
            if (timeSpan.Days > 0)
            {
                fee = _fixedFee;
            }
            return fee;
        }
    }
}