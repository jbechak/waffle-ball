
using System.Data.SqlClient;
using System.Linq.Expressions;
using WaffleBall.Models;

namespace WaffleBall.Dao
{
    public class DBGameDao : IGameDao
    {

        private string connectionString = "Data Source = localhost; Initial Catalog = WaffleBall; Integrated Security = True";
        
        public List<Game> GetGamesByTeam(int teamId)
        {
            var games = new List<Game>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("GetGamesByTeam", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@teamId", teamId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Game game = MapRowToGame(reader);
                                games.Add(game);
                            }
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return games;
          
        }

        public Game GetGameById(int id)
        {
            var game = new Game();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("GetGame", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                game = MapRowToGame(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return game;
        }

        public Game CreateGame(NewGameDto dto)
        {
            var game = new Game();
            int id = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CreateGame", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@homeId", dto.HomeId);
                        command.Parameters.AddWithValue("@visitorId", dto.VisitorId);
                        command.Parameters.AddWithValue("@gameTime", dto.GameTime);

                        id = (int)command.ExecuteScalar();
                        game = GetGameById(id);

                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return game;
        }

        public Game ScoreGame(int id, GameScoreDto dto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("ScoreGame", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@homePoints", dto.HomePoints);
                        command.Parameters.AddWithValue("@visitorPoints", dto.VisitorPoints);
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            return GetGameById(id);
        }

        

        private Game MapRowToGame(SqlDataReader reader)
        {
            var game = new Game();
            game.Id = reader.GetInt32(0);
            game.HomeId= reader.GetInt32(1);
            game.HomeTeam = reader.GetString(2) + " " + reader.GetString(3);
            if (!reader.IsDBNull(4))
            {
                game.HomePoints = reader.GetInt32(4);
            }
            game.VisitorId = reader.GetInt32(5);
            game.VisitorTeam = reader.GetString(6) + " " + reader.GetString(7);
            if (!reader.IsDBNull(8))
            {
                game.VisitorPoints = reader.GetInt32(8);
            }

            
            game.GameTime = reader.GetDateTime(9);
            game.Status = reader.GetString(10);
            

            if (!reader.IsDBNull(11))
            {
                game.WinnerId = reader.GetInt32(11);
            }

            return game;
        }


    }
  
}
