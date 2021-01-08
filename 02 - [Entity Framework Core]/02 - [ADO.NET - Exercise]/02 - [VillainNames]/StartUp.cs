using System;
using System.Data.SqlClient;

namespace VillainNames
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var connectionString = @"Server=DESKTOP-10E0DVG\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

            var connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                var villainNamesQuery =
                @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                    FROM Villains AS v 
                            JOIN MinionsVillains AS mv 
                                ON v.Id = mv.VillainId 
                    GROUP BY v.Id, v.Name 
                        HAVING COUNT(mv.VillainId) > 3 
                    ORDER BY COUNT(mv.VillainId)";

                var villainNamesCommand = new SqlCommand(villainNamesQuery, connection);

                using (villainNamesCommand)
                {
                    var villainsData = villainNamesCommand.ExecuteReader();

                    using (villainsData)
                    {
                        while (villainsData.Read())
                        {
                            Console.WriteLine($"{villainsData["Name"]} - {villainsData["MinionsCount"]}");
                        }
                    }
                }
            }
        }
    }
}
