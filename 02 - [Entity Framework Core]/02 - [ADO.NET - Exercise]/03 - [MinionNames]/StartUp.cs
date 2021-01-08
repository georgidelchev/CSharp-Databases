using System;
using System.Data.SqlClient;

namespace MinionNames
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = @"Server=DESKTOP-10E0DVG\SQLEXPRESS;Database=MinionsDB;Integrated security=true;";

            var connection = new SqlConnection(connectionString);

            var villainId = int.Parse(Console.ReadLine());

            connection.Open();

            using (connection)
            {
                var findAllMinionNamesByVillainIdQuery =
                @"SELECT Name 
                    FROM Villains 
                  WHERE Id = @Id";

                var command = new SqlCommand(findAllMinionNamesByVillainIdQuery, connection);

                command.Parameters.AddWithValue("@Id", villainId);

                using (command)
                {
                    var villainName = command.ExecuteScalar();

                    try
                    {
                        if (villainName == null)
                        {
                            throw new ArgumentException($"No villain with ID {villainId} exists in the database.");
                        }
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);

                        return;
                    }

                    Console.WriteLine(villainName);
                }

                var findVillainMinionsQuery =
                @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                         m.Name, 
                         m.Age
                            FROM MinionsVillains AS mv
                                    JOIN Minions As m 
                                        ON mv.MinionId = m.Id
                          WHERE mv.VillainId = @Id
                  ORDER BY m.Name";

                command = new SqlCommand(findVillainMinionsQuery, connection);

                command.Parameters.AddWithValue("@Id", villainId);

                using (command)
                {
                    var minions = command.ExecuteReader();

                    try
                    {
                        using (minions)
                        {
                            if (!minions.HasRows)
                            {
                                throw new ArgumentException("(no minions)");
                            }

                            while (minions.Read())
                            {
                                Console.WriteLine($"{minions["RowNum"]}. {minions["Name"]}");
                            }
                        }
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                    }
                }
            }
        }
    }
}
