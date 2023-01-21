using WaffleBall.Models;

namespace WaffleBall.Dao
{
    public interface ITeamDao
    {

        List<Team> GetAllTeams();

        Team GetTeam(int id);

        Team UpdateTeamRecord(int id, TeamRecordDto record);
    }
}
