namespace BookStore.Controller
{
    using BookStore.DB;
    using BookStore.View;

    public class IsBook
    {
        public static void CheckExists(int isbn)
        {
            var book = Queries.SelectBook(isbn);
            if (book != null)
                throw new System.ArgumentException($"Book {isbn} exist");
        }

        public static void CheckStatus(int isbn)
        {
            var book = Queries.SelectBook(isbn);
            if (book == null)
                throw new System.ArgumentException($"Book {isbn} does not exist");
            else if (book.Available == -1)
                throw new System.ArgumentException($"Book {isbn} is not available");
        }

        public static void CheckStatus(int isbn, bool update)
        {
            var book = Queries.SelectBook(isbn);
            if (book == null)
                throw new System.ArgumentException($"Book {isbn} does not exist");
            else if (book.Available == 1 && update == true)
                throw new System.ArgumentException($"something went wrong");
        }
    }
}