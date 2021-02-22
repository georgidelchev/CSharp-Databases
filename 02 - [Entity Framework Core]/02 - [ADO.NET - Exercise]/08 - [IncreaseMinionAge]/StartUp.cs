using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _8._Increase_Minion_Age
{
    class StartUp
    {
        private static string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";

        private static SqlConnection connection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            List<int> minionsIDs = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            connection.Open();
            using (connection)
            {
                string queryText = @"UPDATE Minions
                      SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)),
                         Age += 1
                      WHERE Id = @Id";
                SqlCommand command = new SqlCommand(queryText, connection);
                using(command)
                {
                    for (int i = 0; i < minionsIDs.Count; i++)
                    {
                        int id = (int)minionsIDs[i];
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
                queryText = @"SELECT Name, Age FROM Minions";

                command = new SqlCommand(queryText, connection);

                using(command)
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{(string)reader["Name"]} {(int)reader["Age"]}");
                    }
                }

            }
        }
    }
}
