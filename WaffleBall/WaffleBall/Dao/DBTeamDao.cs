
using System.Data.SqlClient;
using System.Linq.Expressions;
using WaffleBall.Models;


namespace WaffleBall.Dao
{
    public class DBTeamDao : ITeamDao
    {
        public List<Team> GetAllTeams()
        {
            var teams = new List<Team>();

            try
            {
                string connectionString = "Data Source = localhost; Initial Catalog = Waffleball; Integrated Security = True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //string sql = "select * from Team;";
                    string sql = "EXEC GetAllTeams;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Team team = MapRowToTeam(reader);
                                teams.Add(team);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return teams;
        }

        public Team GetTeam(int id)
        {
            var team = new Team();

            try
            {
                string connectionString = "Data Source = localhost; Initial Catalog = Waffleball; Integrated Security = True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // THIS WAY USING A SQL QUERY INSTEAD OF A STORED PROCEDURE
                    //string sql = "select * from Team where ID = @id";
                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{
                    //    command.Parameters.AddWithValue("@id", id);
                    //    using (SqlDataReader reader = command.ExecuteReader())
                    //    {
                    //        if (reader.Read())
                    //        {
                    //            team = MapRowToTeam(reader);                          
                    //        }
                    //    }
                    //}

                    using (SqlCommand command = new SqlCommand("GetTeam", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                team = MapRowToTeam(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return team;
        }

        public Team UpdateTeamRecord(int id, TeamRecordDto record)
        {
            try
            {
                string connectionString = "Data Source = localhost; Initial Catalog = WaffleBall; Integrated Security = True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                { 
                    connection.Open();
                    //string sql = "UPDATE Team SET Wins = @wins, Losses = @losses, Points = @points WHERE ID = @id;";
                    //using (SqlCommand command = new SqlCommand(sql, connection))

                    using (SqlCommand command = new SqlCommand("UpdateTeamRecord", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@wins", record.Wins);
                        command.Parameters.AddWithValue("@losses", record.Losses);
                        command.Parameters.AddWithValue("@points", record.Points);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return GetTeam(id);
        
        }




        private Team MapRowToTeam(SqlDataReader reader)
        {
            var team = new Team();
            team.Id = reader.GetInt32(0);
            team.City = reader.GetString(1);
            team.Name = reader.GetString(2);
            team.Conference = reader.GetString(3);
            team.Division = reader.GetString(4);
            team.Wins = reader.GetInt32(5);
            team.Losses = reader.GetInt32(6);
            team.Points = reader.GetInt32(7);

            return team;
        }

    }
}
