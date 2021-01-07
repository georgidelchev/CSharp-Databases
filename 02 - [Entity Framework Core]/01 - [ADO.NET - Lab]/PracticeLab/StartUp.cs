using System;
using System.Data.SqlClient;

namespace PracticeLab
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var connection = new SqlConnection(@"Server=DESKTOP-10E0DVG\SQLEXPRESS;Database=SoftUni;Integrated Security=True;");

            connection.Open();

            using (connection)
            {
                var command1 = new SqlCommand("SELECT * FROM Employees", connection);
                var command2 = new SqlCommand("SELECT COUNT(*) FROM Employees", connection);

                var scalar = command2.ExecuteScalar();
                var reader = command1.ExecuteReader();

                Console.WriteLine(scalar + " records");

                using (reader)
                {
                    while (reader.Read())
                    {
                        var firstName = reader["FirstName"];
                        var lastName = reader["LastName"];
                        var jobTitle = reader["JobTitle"];

                        var result = firstName + " " + lastName + " " + jobTitle;
                        Console.WriteLine(result);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader[i] + " ");
                        }

                        Console.WriteLine();
                    }
                }
            }
        }
    }
}

