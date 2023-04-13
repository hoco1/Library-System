namespace BookStore.View
{
    using BookStore.Model;
    using BookStore.DB;
    using BookStore.Controller;
    // using BookStore.ErrorHandling;
    public class Commandline
    {
        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Book Store");
            Console.WriteLine($"{(int)Menu.AddBook}. Add Book");
            Console.WriteLine($"{(int)Menu.AddPatron}. Add Patron");
            Console.WriteLine($"{(int)Menu.AddBookToPatron}. Add Transaction");
            
            Console.WriteLine($"{(int)Menu.ListTransactions}. List Transactions");
            Console.WriteLine($"{(int)Menu.ListBooks}. List Books");
            Console.WriteLine($"{(int)Menu.ListPatrons}. List Patrons");
            Console.WriteLine($"{(int)Menu.ListBooksForPatron}. List Books for Patron");

            Console.WriteLine($"{(int)Menu.UpdateMembership}. Update Patron");
            Console.WriteLine($"{(int)Menu.UpdateBookFromPatron}. Update Transaction");

            Console.WriteLine($"{(int)Menu.Exit}. Exit");
            Console.WriteLine("Enter Option");
        }
        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
        public static void ShowMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static string GetInput()
        {
            return Console.ReadLine();
        }
        public static int GetInputAsInt()
        {
            return int.Parse(Console.ReadLine());
        }
        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        
        public static Book AddBook()
        {
            try
            {
                Book book = new Book();

                Console.WriteLine("Enter BookId");
                int bookID = GetInputAsInt();
                InputValidation.IsIDValid(bookID);
                IsBook.CheckExists(bookID);
                book.BookId = bookID;

                Console.WriteLine("Enter Title");
                string title = GetInput();
                InputValidation.IsNameValid(title);
                book.Title = title;

                Console.WriteLine("Enter Available");
                int available = GetInputAsInt();
                InputValidation.IsAvailabilityValid(available);
                book.Available = available;

                // insert book to database
                Queries.InsertBook(book);
                ShowSuccess("Book Added Successfully");
                return book;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return null;
            }
        }

        public static void ShowBook(Book book)
        {
            Console.WriteLine($"BookId: {book.BookId}");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Available: {book.Available}");
        }

        // add patron
        public static Patron AddPatron()
        {
            try
            {
                Patron patron = new Patron();
                Console.WriteLine("Enter PatronId");
                int patronID = GetInputAsInt();
                InputValidation.IsIDValid(patronID);
                ISPatron.CheckExists(patronID);
                patron.PatronId = patronID;

                Console.WriteLine("Enter Name");
                string name = GetInput();
                InputValidation.IsNameValid(name);
                patron.Name = name;

                Console.WriteLine("Enter Membership");
                int memberStatus = GetInputAsInt();
                InputValidation.IsMembershipValid(memberStatus);
                patron.MemberStatus = memberStatus;

                patron.Register = DateTime.Now;

                // insert patron to database
                Queries.InsertPatron(patron);

                ShowSuccess("Patron Added Successfully");
                return patron;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return null;
            }
        }

        public static void ShowPatron(Patron patron)
        {
            Console.WriteLine($"PatronId: {patron.PatronId}");
            Console.WriteLine($"Name: {patron.Name}");
            Console.WriteLine($"Status: {patron.MemberStatus}");
            Console.WriteLine($"Register: {patron.Register}");
        }

        public static Transaction AddTransaction()
        {
            try
            {
                Transaction transaction = new Transaction();
                transaction.Checkout();

                Console.WriteLine("Enter BookId");
                int bookID = GetInputAsInt();
                InputValidation.IsIDValid(bookID);
                IsBook.CheckStatus(bookID);
                transaction.BookId = bookID;

                Console.WriteLine("Enter PatronId");
                int patronID = GetInputAsInt();
                InputValidation.IsIDValid(patronID);
                ISPatron.CheckStatus(patronID,true);
                transaction.PatronId = patronID;

                Queries.UpdateBook(transaction.BookId, -1);
                
                // insert transaction to database
                Queries.InsertTransaction(transaction);

                ShowSuccess("Transaction Added Successfully");
                
                return transaction;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return null;
            }
        }

        public static void ShowTransaction(Transaction transaction)
        {
            Console.WriteLine($"BookId: {transaction.BookId}");
            Console.WriteLine($"PatronId: {transaction.PatronId}");
            Console.WriteLine($"DueDate: {transaction.DueDate}");
            Console.WriteLine($"ReturnDate: {transaction.ReturnDate}");
            Console.WriteLine($"DamageFee: {transaction.DamageFee}");
            Console.WriteLine($"LateFee: {transaction.LateFee}");
            Console.WriteLine($"StrategyID: {transaction.StrategyID}");
        }

        
        public static int WhichStrategy()
        {
            Console.WriteLine($"{(int)Strategies.FixedFee}. Fixed Fee");
            Console.WriteLine($"{(int)Strategies.DailyFee}. Daily Fee");
            Console.WriteLine($"{(int)Strategies.WeeklyFee}. Weekly Fee");
            Console.WriteLine($"{(int)Strategies.MonthlyFee}. Monthly Fee");
            Console.WriteLine("Enter Strategy");
            return GetInputAsInt();
        }

        
        public static Transaction UpdateTransaction()
        {
            try
            {
                Transaction transaction = new Transaction();
                // which strategy to use
                int strategy = WhichStrategy();
                transaction.StrategyID = strategy;
                transaction.ChooseStrategy(strategy);

                Console.WriteLine("Enter BookId");
                int bookID = GetInputAsInt();
                InputValidation.IsIDValid(bookID);
                IsBook.CheckStatus(bookID,true);
                transaction.BookId = bookID;

                Console.WriteLine("Enter PatronId");
                int patronID = GetInputAsInt();
                InputValidation.IsIDValid(patronID);
                ISPatron.CheckStatus(patronID,true);
                transaction.PatronId = patronID;

                IsTransaction.CheckExists(transaction.BookId, transaction.PatronId);

                Console.WriteLine("Enter ReturnDate");
                int day = GetInputAsInt();
                transaction.ReturnDate = DateTime.Today.AddDays(day);

                Console.WriteLine("Enter DamageFee");
                transaction.DamageFee = GetInputAsInt();
                transaction.CalculateLateFee();

                // update transaction to database
                Queries.UpdateTransaction(transaction);

                Queries.UpdateBook(transaction.BookId, 1);

                ShowSuccess("Transaction Updated Successfully");

                decimal TotalFee = transaction.Return();
                Console.WriteLine($"Total Fee : {TotalFee}");

                // ask do you want to pay
                Console.WriteLine("Do you want to pay? (y/n)");
                string answer = GetInput();
                if (answer == "y")
                {
                    transaction.Settled();
                    Queries.UpdateTransaction(transaction);
                    ShowSuccess("Transaction Settled Successfully");
                }
                return transaction;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return null;
            }
        }

        public static Patron UpdatePatron()
        {
            int patronID=0;
            try
            {
                ShowInfo("Update Patron");
                Patron patron = new Patron();
                Console.WriteLine("Enter PatronId");
                patronID = GetInputAsInt();
                InputValidation.IsIDValid(patronID);
                ISPatron.CheckStatus(patronID,true);
                patron.PatronId = patronID;

                Console.WriteLine("Enter Name");
                string name = GetInput();
                InputValidation.IsNameValid(name);
                patron.Name = name;

                Console.WriteLine("Enter membership status (1,3,6,12)");
                int memberStatus = GetInputAsInt();
                InputValidation.IsMembershipValid(memberStatus);
                patron.MemberStatus = memberStatus;

                patron.Register = DateTime.Now;

                // update patron to database
                Queries.UpdatePatron(patron);

                ShowSuccess("Patron Updated Successfully");

                return patron;
            }catch (Exception ex)
            {
                ShowError(ex.Message);

                if (ex.Message == "Patron not found")
                {
                    ShowError(ex.Message);
                    return null;
                }else if (ex.Message == "Patron, subscription expired")
                {
                    Console.WriteLine("Do you want to renew subscription? (y/n)");
                    string answer = GetInput();
                    if (answer == "y")
                    {
                        try
                        {
                            Console.WriteLine("Enter membership status (1,3,6,12)");
                            int membershipStatus = GetInputAsInt();
                            InputValidation.IsMembershipValid(membershipStatus);
                            Utility.RenewSubscription(patronID, membershipStatus);
                            ShowSuccess("Subscription renewed successfully");
                            return null;
                        }
                        catch (Exception e)
                        {
                            ShowError(e.Message);
                            return null;
                        }
                    }
                }else if (ex.Message == "Patron, has unpaid fee")
                {
                    Console.WriteLine("Do you want to pay? (y/n)");
                    string answer = GetInput();
                    if (answer == "y")
                    {

                        Utility.PayAllFees(patronID);
                        ShowSuccess("Fee paid successfully");
                        return null;

                    }
                }
                return null;
            }
        }

        public static void ShowTransaction()
        {
            ShowInfo("Transactions for a particular patron");
            Console.WriteLine("Enter Patron Id");
            int patronId = GetInputAsInt();
            Patron patron = Queries.SelectPatron(patronId);
            if (patron == null)
            {
                ShowError("Patron not found");
                return;
            }
            ShowTransactionList(Queries.SelectTransactions(patronId));
            // show total fee
            decimal totalFee = Utility.CalculateTotalFee(patronId);
            Console.WriteLine($"Total Fee: {totalFee}");
        }

        public static void ShowBookList(List<Book> books)
        {
            ShowInfo("Books List");
            foreach (var book in books)
            {
                ShowBook(book);
                Console.WriteLine("==================================");
            }
        }

        public static void ShowPatronList(List<Patron> patrons)
        {
            ShowInfo("Patrons List");
            foreach (var patron in patrons)
            {
                ShowPatron(patron);
                Console.WriteLine("==================================");
            }
        }

        public static void ShowTransactionList(List<Transaction> transactions)
        {
            ShowInfo("Transactions List");
            foreach (var transaction in transactions)
            {
                ShowTransaction(transaction);
                Console.WriteLine("==================================");
            }
        }

    }
}