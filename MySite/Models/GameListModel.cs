using Microsoft.AspNetCore.Mvc;

namespace MySite.Models
{
	public class GameListModel 
	{
		public IEnumerable<GameOfUser>? GamesOfUsers { get; set; }

		public GameOfUser? GamesOfUser { get; set; }
	}
}
