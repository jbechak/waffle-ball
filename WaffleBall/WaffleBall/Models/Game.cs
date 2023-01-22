namespace WaffleBall.Models
{
    public class Game
    {

        public int Id { get; set; }
        public int HomeId { get; set; }

        public string HomeTeam { get; set; }

        public int? HomePoints { get; set; }

        public int VisitorId { get; set; }

        public string VisitorTeam { get; set; }

        public int? VisitorPoints { get; set; }

        public DateTime GameTime { get; set; }

        public string? Status { get; set; }

        public int? WinnerId { get; set; }

        public string? WinnerTeam
        {
            get
            {
                if (Status == null || !Status.Equals("FINISHED"))
                {
                    return null;
                }
                return WinnerId == HomeId ? HomeTeam : VisitorTeam;
            }
        }

    }
}
