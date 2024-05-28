using Microsoft.AspNetCore.Mvc;

namespace MySite.Models
{
	public class GameListModel 
	{
		public IEnumerable<GamesOfUser>? GamesOfUsers { get; set; }

		public GamesOfUser? GamesOfUser { get; set; }
	}
}
