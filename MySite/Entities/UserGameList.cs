using MySite.Models;

namespace MySite.Entities
{
	public class UserGameList
	{
		public int Id { get; set; }
		public string? Game { get; set; }
		public Guid ShareableLink { get; set; } = Guid.NewGuid(); // Unique identifier for sharing
		public ICollection<Game>? Games { get; set; }
		public ICollection<Comment>? Comments { get; set; }
		public Person? Person { get; set; }
	}
}
