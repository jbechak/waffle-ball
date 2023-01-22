using Microsoft.AspNetCore.Mvc;
using WaffleBall.Dao;
using WaffleBall.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WaffleBall.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private IGameDao dao = new DBGameDao();

       
        [HttpGet("team/{id}")]
        public List<Game> GetGamesByTeam(int id)
        {
            return dao.GetGamesByTeam(id);
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public Game GetGameById(int id)
        {
            return dao.GetGameById(id);
        }

        // POST api/<GameController>
        [HttpPost]
        public Game CreateGame([FromBody] NewGameDto dto)
        {
            return dao.CreateGame(dto);
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public Game ScoreGame(int id, [FromBody] GameScoreDto dto)
        {
            return dao.ScoreGame(id, dto);
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
