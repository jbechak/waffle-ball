namespace WaffleBall.Models
{
    public class Team
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Conference { get; set; } = string.Empty;

        public string Division { get; set; } = string.Empty;

        public int Wins { get; set; }

        public int Losses { get; set; } 

        public int Points { get; set; }


    }
}
