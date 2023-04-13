namespace BookStore.Model
{
    public class Transaction
    {
        public int BookId { get; set; }
        public string? BookName { get; set; }
        public int PatronId { get; set; }
        public string? PatronName { get; set; }
        public DateTime DueDate;
        public DateTime? ReturnDate { get; set; }
        public Decimal DamageFee { get; set; }
        public Decimal LateFee { get; set; }
        public Decimal TotalFee { get; set; }
        public int StrategyID { get; set; }

        public ICalculateFeeStrategy CalculateFeeStrategy { get; set; }


        public void ChooseStrategy(int strategy)
        {
            switch (strategy)
            {
                case 1:
                    CalculateFeeStrategy = new FixedFeeStrategy(100);
                    break;
                case 2:
                    CalculateFeeStrategy = new DailyFeeStrategy();
                    break;
                case 3:
                    CalculateFeeStrategy = new WeeklyFeeStrategy();
                    break;
                case 4:
                    CalculateFeeStrategy = new MonthlyFeeStrategy();
                    break;
                default:
                    CalculateFeeStrategy = new DailyFeeStrategy();
                    break;
            }
        }

        public void Checkout()
        {
            // Logic for checking out a book
            DueDate = DateTime.Today.AddDays(14); // Books are due in 14 days
            
        }
        
        // calculate late fee for patron
        public decimal CalculateLateFee()
        {
            if (!ReturnDate.HasValue)
            {
                return 0;
            }
            LateFee = CalculateFeeStrategy.CalculateFee(DueDate, ReturnDate.Value);
            return LateFee;
        }

        public decimal Return()
        {
            // LateFee = 0;

            if (!ReturnDate.HasValue)
            {
                return 0;
            }
            LateFee = CalculateFeeStrategy.CalculateFee(DueDate, ReturnDate.Value);

            TotalFee = LateFee + DamageFee;
            return TotalFee;
        }

        // settled transaction
        public void Settled()
        {
            // Logic for settling a transaction
            DamageFee = 0;
            LateFee = 0;
        }

    }
}