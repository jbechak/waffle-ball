using WaffleBall.Models;

namespace WaffleBall.Dao
{
    public interface ITeamDao
    {

        List<Team> GetAllTeams();

        Team GetTeam(int id);

        Team CreateTeam(TeamDto dto);

        Team UpdateTeamRecord(int id, TeamRecordDto record);
    }
}
