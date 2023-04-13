using BookStore.View;
using BookStore.DB;
public class Program
{
    public static void Main(string[] args)
    {
        Tables.CreateTables();

        while (true)
        {
                Commandline.ShowMenu();

                int option = 10;

                try
                {
                    option = Commandline.GetInputAsInt();
                }
                catch (Exception e)
                {
                    Commandline.ShowError(e.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                switch (option)
                {
                    case (int)Menu.AddBook:
                        Commandline.AddBook();
                        break;
                    case (int)Menu.AddPatron:
                        Commandline.AddPatron();
                        break;
                    case (int)Menu.AddBookToPatron:
                        Commandline.AddTransaction();
                        break;
                    case (int)Menu.UpdateBookFromPatron:
                        Commandline.UpdateTransaction();
                        break;
                    case (int)Menu.ListTransactions:
                        Commandline.ShowTransactionList(Queries.SelectTransactions());
                        break;
                    case (int)Menu.ListBooks:
                        Commandline.ShowBookList(Queries.SelectBooks());
                        break;
                    case (int)Menu.ListPatrons:
                        Commandline.ShowPatronList(Queries.SelectPatrons());
                        break;
                    case (int)Menu.ListBooksForPatron:
                        Commandline.ShowTransaction();
                        break;
                    case (int)Menu.UpdateMembership:
                        Commandline.UpdatePatron();
                        break;
                    case (int)Menu.Exit:
                        return;
                    default:
                        Commandline.ShowError("Invalid Option");
                        break;
                }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();   
        }
    }
}

