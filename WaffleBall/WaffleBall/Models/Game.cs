namespace WaffleBall.Models
{
    public class Game
    {

        public int Id { get; set; }
        public int HomeId { get; set; }

        public string HomeCity { get; set; }

        public string HomeTeamName { get; set; }

        public int HomePoints { get; set; }

        public int VisitorId { get; set; }

        public string VisitorCity { get; set; }

        public string VisitorTeamName { get; set; }

       
        public int VisitorPoints { get; set; }

        public DateTime GameTime { get; set; }

    }
}
