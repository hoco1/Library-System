namespace BookStore.DB
{
    using System.Configuration;
    using System.Data.SqlClient;
    using BookStore.Model;

    public class Queries
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
        private static SqlConnection _connection = new SqlConnection(_connectionString);

        
        public static void InsertPatron(Patron patron)
        {

                _connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;

                command.CommandText = @"INSERT INTO Patrons (PatronId, Name, Register, MemberStatus) VALUES 
                                        (@PatronId, @Name, @Register, @MemberStatus)";
                command.Parameters.AddWithValue("@PatronId", patron.PatronId);
                command.Parameters.AddWithValue("@Name", patron.Name);
                command.Parameters.AddWithValue("@Register", patron.Register);
                command.Parameters.AddWithValue("@MemberStatus", patron.MemberStatus);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    _connection.Close();
                }
          
        }

        
        public static void InsertBook(Book book)
        {

                _connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;

                command.CommandText = @"INSERT INTO Books (BookId, Title, Available) VALUES 
                                        (@BookId, @Title, @Available)";
                command.Parameters.AddWithValue("@BookId", book.BookId);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Available", book.Available);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    _connection.Close();
                }
           
        }

        
        public static void InsertTransaction(Transaction transaction)
        {
                _connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;

                command.CommandText = @"INSERT INTO Transactions (PatronId, BookId, DueDate) VALUES 
                                        (@PatronId, @BookId, @DueDate)";
                command.Parameters.AddWithValue("@PatronId", transaction.PatronId);
                command.Parameters.AddWithValue("@BookId", transaction.BookId);
                command.Parameters.AddWithValue("@DueDate", transaction.DueDate);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    _connection.Close();
                }
          
        }
    
        
        public static void UpdateTransaction(Transaction transaction)
        {

                _connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;

                command.CommandText = @"UPDATE Transactions SET ReturnDate = @ReturnDate, 
                                        DamageFee = @DamageFee, 
                                        LateFee = @LateFee ,
                                        StrategyID = @StrategyID
                                        WHERE PatronId = @PatronId AND BookId = @BookId";
                command.Parameters.AddWithValue("@PatronId", transaction.PatronId);
                command.Parameters.AddWithValue("@BookId", transaction.BookId);
                command.Parameters.AddWithValue("@ReturnDate", transaction.ReturnDate);
                command.Parameters.AddWithValue("@DamageFee", transaction.DamageFee);
                command.Parameters.AddWithValue("@LateFee", transaction.LateFee);
                command.Parameters.AddWithValue("@StrategyID", transaction.StrategyID);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    _connection.Close();
                }
           
        }

        public static void UpdatePatron(Patron patron)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = @"UPDATE Patrons SET Name = @Name, Register = @Register, MemberStatus = @MemberStatus WHERE PatronId = @PatronId";
            command.Parameters.AddWithValue("@PatronId", patron.PatronId);
            command.Parameters.AddWithValue("@Name", patron.Name);
            command.Parameters.AddWithValue("@Register", patron.Register);
            command.Parameters.AddWithValue("@MemberStatus", patron.MemberStatus);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        
        public static void UpdateBook(int bookId, int available)
        {
            _connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = @"UPDATE Books SET Available = @Available WHERE BookId = @BookId";
            command.Parameters.AddWithValue("@BookId", bookId);
            command.Parameters.AddWithValue("@Available", available);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        
        public static List<Book> SelectBooks()
        {
            List<Book> books = new List<Book>();
            _connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = @"SELECT * FROM Books";

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookId = (int)reader["BookId"];
                    book.Title = (string)reader["Title"];
                    book.Available = (int)reader["Available"];
                    books.Add(book);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return books;
        }
        
        public static List<Patron> SelectPatrons()
        {
            List<Patron> patrons = new List<Patron>();
            _connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = @"SELECT * FROM Patrons";

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Patron patron = new Patron();
                    patron.PatronId = (int)reader["PatronId"];
                    patron.Name = (string)reader["Name"];
                    patron.Register = (DateTime)reader["Register"];
                    patron.MemberStatus = (int)reader["MemberStatus"];
                    patrons.Add(patron);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return patrons;
        }


        public static List<Transaction> SelectTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            _connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = @"SELECT * FROM Transactions";

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Transaction transaction = new Transaction();
                    transaction.BookId = reader.GetInt32(1);
                    transaction.PatronId = reader.GetInt32(2);
                    transaction.DueDate = reader.GetDateTime(3);
                    // check if the value is null
                    if (!reader.IsDBNull(4))
                        transaction.ReturnDate = reader.GetDateTime(4);
                    if (!reader.IsDBNull(5))
                        transaction.DamageFee = reader.GetDecimal(5);
                    if (!reader.IsDBNull(6))
                        transaction.LateFee = reader.GetDecimal(6);
                    if (!reader.IsDBNull(7))
                        transaction.StrategyID = reader.GetInt32(7);
                    transactions.Add(transaction);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return transactions;
        }

        
        public static List<Transaction> SelectTransactions(int patronId)
        {
            List<Transaction> transactions = new List<Transaction>();
            _connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = @"SELECT th.BookId,Books.Title,
			                        th.PatronId,Patrons.Name,
			                        th.DueDate,th.ReturnDate,
                                    th.DamageFee,th.LateFee,
                                    th.StrategyID
                                    FROM Transactions AS th
                                    INNER JOIN Books ON th.BookId = Books.BookId
                                    INNER JOIN Patrons ON th.PatronId = Patrons.PatronId
                                    WHERE th.PatronId= @PatronId";

            command.Parameters.AddWithValue("@PatronId", patronId);

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Transaction transaction = new Transaction();
                    transaction.BookId = reader.GetInt32(0);
                    transaction.BookName = reader.GetString(1);
                    transaction.PatronId = reader.GetInt32(2);
                    transaction.PatronName = reader.GetString(3);
                    transaction.DueDate = reader.GetDateTime(4);
                    if (!reader.IsDBNull(5))
                        transaction.ReturnDate = reader.GetDateTime(5);
                    if (!reader.IsDBNull(6))
                        transaction.DamageFee = reader.GetDecimal(6);
                    if (!reader.IsDBNull(7))
                        transaction.LateFee = reader.GetDecimal(7);
                    if (!reader.IsDBNull(8))
                        transaction.StrategyID = reader.GetInt32(8);
                    transactions.Add(transaction);
                }

                reader.Close();
                    
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                
                _connection.Close();
            }
            return transactions;
        }

        
        public static Patron SelectPatron(int patronId)
        {
            Patron patron = new Patron();
            _connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = @"SELECT * FROM Patrons WHERE PatronId = @PatronId";
            command.Parameters.AddWithValue("@PatronId", patronId);

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        patron.PatronId = (int)reader["PatronId"];
                        patron.Name = (string)reader["Name"];
                        patron.Register = (DateTime)reader["Register"];
                        patron.MemberStatus = (int)reader["MemberStatus"];
                    }
                }else
                {
                    patron = null;
                }
                
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return patron;
        }

        public static Book SelectBook(int bookId)
        {
            Book book = new Book();
            _connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = @"SELECT * FROM Books WHERE BookId = @BookId";
            command.Parameters.AddWithValue("@BookId", bookId);

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.BookId = (int)reader["BookId"];
                        book.Title = (string)reader["Title"];
                        book.Available = (int)reader["Available"];
                    }
                }else
                {
                    book = null;
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return book;
        }

        // select a transaction
        public static Transaction SelectTransaction(int patronId, int bookId)
        {
            Transaction transaction = new Transaction();
            _connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _connection;

            command.CommandText = @"SELECT * FROM Transactions WHERE PatronId = @PatronId AND BookId = @BookId";
            command.Parameters.AddWithValue("@PatronId", patronId);
            command.Parameters.AddWithValue("@BookId", bookId);

            try
            {
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        transaction.BookId = reader.GetInt32(1);
                        transaction.PatronId = reader.GetInt32(2);
                        transaction.DueDate = reader.GetDateTime(3);
                        // check if the value is null
                        if (!reader.IsDBNull(4))
                            transaction.ReturnDate = reader.GetDateTime(4);
                        if (!reader.IsDBNull(5))
                            transaction.DamageFee = reader.GetDecimal(5);
                        if (!reader.IsDBNull(6))
                            transaction.LateFee = reader.GetDecimal(6);
                        if (!reader.IsDBNull(7))
                            transaction.StrategyID = reader.GetInt32(7);
                    }
                }
                else
                {
                    transaction = null;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return transaction;
        }
        

    }
}