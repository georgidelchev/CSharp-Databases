using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _5._Change_Town_Names_Casing
{
   public class StartUp
    {
        private static string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";

        private static SqlConnection connection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            string country = Console.ReadLine();

            connection.Open();

            using(connection)
            {
                SqlCommand command = new SqlCommand(Queries.ChangeCityNames, connection);
                command.Parameters.AddWithValue("@countryName", country);

                using(command)
                {
                    int affectedRows = command.ExecuteNonQuery();
                    
                    if(affectedRows==0)
                    {
                        Console.WriteLine("No town names were affected.");
                    }
                    else
                    {
                        Console.WriteLine($"{affectedRows} town names were affected.");

                        PrintCityNames(country);
                    }
                }
            }
        }

        private static void PrintCityNames(string countryName)
        {
            SqlCommand command = new SqlCommand(Queries.FindAllCityNames, connection);
            command.Parameters.AddWithValue("@countryName", countryName);

            using(command)
            {
                SqlDataReader reader = command.ExecuteReader();
                List<string> cityNames = new List<string>();
                while (reader.Read())
                {
                    cityNames.Add((string)reader["Name"]);
                }

                Console.WriteLine($"[{string.Join(", ",cityNames)}]");
            }
        }
    }
}
