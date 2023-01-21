using Microsoft.AspNetCore.Mvc;
using WaffleBall.Dao;
using WaffleBall.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WaffleBall.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        private ITeamDao dao = new DBTeamDao();

       
        [HttpGet]
        public List<Team> GetAllTeams()
        {
            Console.WriteLine("Team Controller Get All");
            return dao.GetAllTeams();
        }

        
        [HttpGet("{id}")]
        public Team Get(int id)
        {
            return dao.GetTeam(id);
        }

      
        [HttpPost]
        public Team Post([FromBody] TeamDto dto)
        {
            return dao.CreateTeam(dto);
        }

        
        [HttpPut("{id}")]
        public Team Put(int id, [FromBody] TeamRecordDto record)
        {
            return dao.UpdateTeamRecord(id, record);
        }

   
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
