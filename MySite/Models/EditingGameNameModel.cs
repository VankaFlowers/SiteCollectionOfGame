namespace MySite.Models
{
    public class EditingGameNameModel
    {
        public string? CurrentGameName { get; set; }
        public string? NewGameName { get; set; }
        public int? GameId { get; set; }

        public string? Alert{ get; set; }
    }
}
