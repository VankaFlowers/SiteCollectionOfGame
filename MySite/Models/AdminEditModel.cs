namespace MySite.Models
{
	public class AdminEditModel
	{
		public string? GameName { get; set; }
		public int? GameId { get; set; }
		public string? GenreName { get; set; }

        public ICollection<string>? Games { get; set; }

        public GameDescription.GameDescriptionModel? GameDescription { get; set; }
		public CreatingStatusModel? StatusModel { get; set; }
		public string? Text{ get; set; }
    }
}
