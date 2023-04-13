namespace BookStore.Controller
{
    using BookStore.DB;
    using BookStore.View;

    public class IsTransaction
    {
        public static void CheckExists(int bookId,int patronId)
        {
            var transaction = Queries.SelectTransaction(patronId,bookId);
            if (transaction == null)
            {
                throw new System.ArgumentException($"Transaction does not exist");
            }
        }
    }


}