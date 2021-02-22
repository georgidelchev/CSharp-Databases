using Microsoft.Data.SqlClient;
using System;

namespace _9._Increase_Age_Stored_Procedure
{
    class Program
    {
        private static string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";

        private static SqlConnection connection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            connection.Open();
            using (connection)
            {
                string queryText = @"CREATE PROC usp_GetOlder (@id INT)
                                    AS
                                        UPDATE Minions
                                        SET Age += 1
                                        WHERE Id = @id";
                SqlCommand command = new SqlCommand(queryText, connection);
                int id = int.Parse(Console.ReadLine());
                
                using(command)
                {
                    command.ExecuteNonQuery();
                }
                queryText = @"EXEC usp_GetOlder @id";
                command = new SqlCommand(queryText, connection);
                command.Parameters.AddWithValue("@id", id);

                using(command)
                {
                    command.ExecuteNonQuery();
                }

                queryText = @"SELECT Name, Age FROM Minions WHERE Id = @Id";
                command = new SqlCommand(queryText, connection);
                command.Parameters.AddWithValue("@id", id);

                using(command)
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{(string)reader["Name"]} - {(int)reader["Age"]} years old");
                    }
                }
            }
        }
    }
}
