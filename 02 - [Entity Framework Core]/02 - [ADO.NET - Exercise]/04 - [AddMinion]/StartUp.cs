//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace _4._Add_Minion
//{
//    class StartUp
//    {
//        static void Main(string[] args)
//        {
//            List<string> minionsData = ReadData();
//            string villainName = ReadData()[0];
//            string minionName = minionsData[0];
//            int age = int.Parse(minionsData[1]);
//            string town = minionsData[2];

//            string connectionString = @"Server=.;Database=MinionsDB;Integrated Security=true";
//            SqlConnection sqlConnection = new SqlConnection(connectionString);
//            sqlConnection.Open();
//            using (sqlConnection)
//            {
//                //SqlTransaction sqlTransaction = sqlConnection.BeginTransaction("AddMinionToVillain");

//                int townId = FindTownId(town, sqlConnection);

//                int villainID = FindVillainID(villainName, sqlConnection);

//                int minionId = FindMinionId(minionName, age, sqlConnection, townId);

//                InsertMinionToVillain(minionId, villainID, minionName, villainName, sqlConnection);

//                //sqlTransaction.Commit();

//            }
//        }

//        private static void InsertMinionToVillain(int minionId, int villainId, string minionName, string villainName, SqlConnection sqlConnection)
//        {
//            string queryText = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";
//            SqlCommand command = new SqlCommand(queryText, sqlConnection);
//            command.Parameters.AddWithValue("@minionId", minionId);
//            command.Parameters.AddWithValue("@villainId", villainId);

//            using (command)
//            {
//                try
//                {
//                    command.ExecuteNonQuery();
//                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("There was an error inserting this minion to this villain!");
//                    Console.WriteLine(e.Message);
//                }
//            }
//        }

//        private static int FindMinionId(string minionName, int age, SqlConnection sqlConnection, int townId)
//        {
//            int minionId = 0;
//            string queryText = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@name,@age, @townId)";
//            SqlCommand insertMinionCmd = new SqlCommand(queryText, sqlConnection);
//            insertMinionCmd.Parameters.AddWithValue("@name", minionName);
//            insertMinionCmd.Parameters.AddWithValue("@age", age);
//            insertMinionCmd.Parameters.AddWithValue("@townId", townId);
//            using (insertMinionCmd)
//            {
//                try
//                {
//                    insertMinionCmd.ExecuteNonQuery();
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("There was a n error inserting this minion!");
//                    Console.WriteLine(e.Message);
//                }
//            }
//            queryText = "SELECT Id FROM Minions WHERE Name = @Name";
//            SqlCommand findMinionIdCmd = new SqlCommand(queryText, sqlConnection);
//            findMinionIdCmd.Parameters.AddWithValue("@Name", minionName);

//            using (findMinionIdCmd)
//            {
//                try
//                {
//                    minionId = (int)findMinionIdCmd.ExecuteScalar();
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("There was an error searchind the Id of this minion!");
//                    Console.WriteLine(e.Message);
//                }
//            }

//            return minionId;
//        }

//        private static int FindTownId(string town, SqlConnection sqlConnection)
//        {
//            int townID = 0;
//            string queryText = @"SELECT COUNT(*) FROM Towns WHERE @townName= Name";
//            SqlCommand command = new SqlCommand(queryText, sqlConnection);
//            command.Parameters.AddWithValue("@townName", town);
//            using (command)
//            {
//                try
//                {
//                    if ((int)command.ExecuteScalar() == 0)
//                    {
//                        queryText = @"INSERT INTO Towns (Name) VALUES (@townName)";
//                        SqlCommand insertTownCmd = new SqlCommand(queryText, sqlConnection);
//                        insertTownCmd.Parameters.AddWithValue("@townName", town);
//                        using (insertTownCmd)
//                        {
//                            try
//                            {
//                                insertTownCmd.ExecuteNonQuery();
//                                Console.WriteLine($"Town {town} was added to the database.");
//                            }
//                            catch (Exception e)
//                            {
//                                Console.WriteLine("There was an error inserting town!");
//                                Console.WriteLine(e.Message);
//                            }


//                        }
//                    }
//                    queryText = @"SELECT Id FROM Towns WHERE @townName=Name";
//                    SqlCommand findTownIdCmd = new SqlCommand(queryText, sqlConnection);
//                    findTownIdCmd.Parameters.AddWithValue("@townName", town);
//                    townID = (int)findTownIdCmd.ExecuteScalar();
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("There was an error searching in Towns table!");
//                    Console.WriteLine(e.Message);
//                }

//            }
//            return townID;
//        }

//        private static int FindVillainID(string villainName, SqlConnection sqlConnection)
//        {
//            string queryText = @"SELECT Count(*) FROM Villains WHERE Name = @Name";
//            SqlCommand findVillainIdByName = new SqlCommand(queryText, sqlConnection);
//            findVillainIdByName.Parameters.AddWithValue("@Name", villainName);
//            int villainID = 0;
//            using (findVillainIdByName)
//            {
//                try
//                {
//                    if ((int)findVillainIdByName.ExecuteScalar() == 0)
//                    {
//                        queryText = @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
//                        SqlCommand insertVillain = new SqlCommand(queryText, sqlConnection);
//                        insertVillain.Parameters.AddWithValue("@villainName", villainName);
//                        using (insertVillain)
//                        {
//                            try
//                            {
//                                insertVillain.ExecuteNonQuery();
//                                Console.WriteLine($"Villain {villainName} was added to the database.");


//                            }
//                            catch (Exception e)
//                            {
//                                Console.WriteLine("There was an error inserting this villain!");
//                                Console.WriteLine(e.Message);
//                            }
//                        }
//                    }

//                    villainID = (int)findVillainIdByName.ExecuteScalar();

//                }
//                catch (Exception e)
//                {

//                    Console.WriteLine("There was an error searching this villain!");
//                    Console.WriteLine(e.Message);
//                }

//            }
//            return villainID;

//        }

//        private static List<string> ReadData() =>
//            Console.ReadLine()
//            .Split(new string[] { " ", ":" }, StringSplitOptions.RemoveEmptyEntries)
//            .Skip(1)
//            .ToList();


//    }
//}
