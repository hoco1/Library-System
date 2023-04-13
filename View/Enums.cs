namespace BookStore.View
{
    public enum Menu
    {
        AddBook=1,
        AddPatron,
        AddBookToPatron,
        
        ListTransactions,
        ListBooks,
        ListPatrons,
        ListBooksForPatron,
        UpdateMembership,
        UpdateBookFromPatron,
        Exit
    }

    public enum Strategies
    {
        FixedFee=1,
        DailyFee,
        WeeklyFee,
        MonthlyFee
    }
}