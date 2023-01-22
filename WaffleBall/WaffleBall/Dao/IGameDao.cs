using WaffleBall.Models;

namespace WaffleBall.Dao
{
    public interface IGameDao
    {

        List<Game> GetGamesByTeam(int teamId);

        Game GetGameById(int id);

        Game CreateGame(NewGameDto dto);

        Game ScoreGame(int id, GameScoreDto dto);
    }
}
