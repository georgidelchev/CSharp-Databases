using System;
using System.Data.SqlClient;

namespace RemoveVillain
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var connectionString = @"Server=DESKTOP-10E0DVG\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

            var connection = new SqlConnection(connectionString);

            SqlTransaction transaction;

            connection.Open();

            int id = int.Parse(Console.ReadLine());

            using (connection)
            {
                transaction = connection.BeginTransaction();

                try
                {
                    var findVillainIdQuery =
                        @"SELECT Name 
                            FROM Villains 
                        WHERE Id = @villainId";

                    var command = new SqlCommand();

                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandText = findVillainIdQuery;

                    command.Parameters.AddWithValue("@villainId", id);

                    object value = command.ExecuteScalar();

                    if (value == null)
                    {
                        throw new ArgumentException("No such villain was found.");
                    }

                    var villainName = (string)value;

                    var deleteMinionsVillainsFromIdQuery =
                        @"DELETE FROM MinionsVillains 
                            WHERE VillainId = @villainId";

                    command.CommandText = deleteMinionsVillainsFromIdQuery;

                    var countOfDeletedMinions = command.ExecuteNonQuery();

                    var deleteVillainFromIdQuery =
                        @"DELETE FROM Villains
                            WHERE Id = @villainId";

                    command.CommandText = deleteVillainFromIdQuery;

                    command.ExecuteNonQuery();

                    transaction.Commit();

                    Console.WriteLine($"{villainName} was deleted.");

                    Console.WriteLine($"{countOfDeletedMinions} minions were released.");
                }
                catch (ArgumentException ae)
                {
                    try
                    {
                        Console.WriteLine(ae.Message);

                        transaction.Rollback();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                catch (Exception e)
                {
                    try
                    {
                        Console.WriteLine(e.Message);

                        transaction.Rollback();
                    }
                    catch (Exception re)
                    {
                        Console.WriteLine(re.Message);
                    }
                }
            }
        }
    }
}
