namespace BookStore.Model
{
    using BookStore.DB;
    public class Utility
    {
        // calculate total fee for patron
        public static decimal CalculateTotalFee(int PatronId)
        {
            decimal totalFee = 0;
            List<Transaction> transactions = Queries.SelectTransactions(PatronId);
            
            foreach (Transaction transaction in transactions)
            {      
                totalFee += transaction.LateFee + transaction.DamageFee;
            }
            return totalFee;
        }

        // pay all fees for patron
        public static void PayAllFees(int PatronId)
        {
            List<Transaction> transactions = Queries.SelectTransactions(PatronId);
            foreach (var transaction in transactions)
            {
                transaction.ReturnDate = DateTime.Now;
                transaction.Settled();
                Queries.UpdateTransaction(transaction);
            }
        }

        // renew subscription for patron
        public static void RenewSubscription(int PatronId,int MembershipStatus)
        {
            Patron patron = Queries.SelectPatron(PatronId);
            patron.Register = DateTime.Now;
            patron.MemberStatus = MembershipStatus;
            Queries.UpdatePatron(patron);
        }
    }

}