using Microsoft.AspNetCore.Mvc.Rendering;

namespace MySite.Models
{
    public class GameOfPerson
    {
        public string? NameOfGame { get; set; }

        public string? SelectedGameId { get; set; }
        public string? Comment { get; set; }

        public SelectList? Games { get; set; }
    }
}
