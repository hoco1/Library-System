namespace BookStore.DB
{
    using System.Configuration;
    using System.Data.SqlClient;
    public class Tables
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
        private static SqlConnection _connection = new SqlConnection(_connectionString);

        public static void CreateTables()
        {
                _connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = _connection;

                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Books]') AND type in (N'U')) 
                                        BEGIN 
                                            CREATE TABLE [dbo].[Books] ( 
                                                [BookId] INT NOT NULL PRIMARY KEY, 
                                                [Title] NVARCHAR(50) NOT NULL, 
                                                [Available] INT NOT NULL ) 
                                        END";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }

                // insert values into Books table
                
                command.CommandText = @"INSERT INTO Books (BookId, Title, Available) VALUES 
                                        (11, 'The Hobbit', -1),
                                        (21, 'The Lord of the Rings', -1),
                                        (31, 'The Silmarillion', -1),
                                        (41, 'The Children of Hurin', -1),
                                        (51, 'The Fall of Gondolin', -1),
                                        (61, 'The Unfinished Tales', -1),
                                        (71, 'The History of Middle Earth', -1),
                                        (81, 'The Book of Lost Tales', -1),
                                        (91, 'The Book of Lost Tales Part 2', -1),
                                        (12, 'The Book of Lost Tales Part 3', -1)";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }

                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patrons]') AND type in (N'U'))
                                        BEGIN
                                            CREATE TABLE [dbo].[Patrons] (
                                                [PatronId] INT NOT NULL PRIMARY KEY,
                                                [Name] NVARCHAR(50) NOT NULL,
                                                [Register] DATETIME NOT NULL,
                                                [MemberStatus] INT NOT NULL
                                                )
                                        END";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }

                // insert values into Patrons table
                command.CommandText = @"INSERT INTO Patrons (PatronId, Name, Register, MemberStatus) VALUES
                                        (1, 'Frodo Baggins', '2019-01-01', 12),
                                        (2, 'Samwise Gamgee', '2019-01-01', 8),
                                        (3, 'Meriadoc Brandybuck', '2019-01-01', 10),
                                        (4, 'Peregrin Took', '2019-01-01', 6),
                                        (5, 'Gandalf the Grey', '2019-01-01', 10),
                                        (6, 'Gandalf the White', '2019-01-01', 2),
                                        (7, 'Bilbo Baggins', '2019-01-01', 9),
                                        (8, 'Elrond', '2019-01-01', 10),
                                        (9, 'Galadriel', '2019-01-01', 10),
                                        (10, 'Saruman the White', '2019-01-01', 10)";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }

                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions]') AND type in (N'U'))
                                        BEGIN
                                            CREATE TABLE [dbo].[Transactions] (
                                            [TransactionId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
                                            [BookId] INT NOT NULL,
                                            [PatronId] INT NOT NULL,
                                            [DueDate] DATETIME NOT NULL,
                                            [ReturnDate] DATETIME,
                                            [DamageFee] DECIMAL(18,2),
                                            [LateFee] DECIMAL(18,2),
                                            [StrategyID] INT DEFAULT 0,
                                            FOREIGN KEY (BookId) REFERENCES Books(BookId),
                                            FOREIGN KEY (PatronId) REFERENCES Patrons(PatronId)
                                            )
                                        END";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }

                // insert values into Transactions table
                command.CommandText = @"INSERT INTO Transactions (BookId, PatronId, DueDate) VALUES
                                        (11, 1, '2019-01-01'),
                                        (21, 2, '2019-01-01'),
                                        (31, 3, '2019-01-01'),
                                        (41, 4, '2019-01-01'),
                                        (51, 5, '2019-01-01'),
                                        (61, 6, '2019-01-01'),
                                        (71, 7, '2019-01-01'),
                                        (81, 8, '2019-01-01'),
                                        (91, 9, '2019-01-01'),
                                        (12, 10, '2019-01-01')";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }

                _connection.Close();
            
        }
    }
}