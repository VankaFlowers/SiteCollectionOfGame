using MySite.Entities;

namespace MySite.Models.GameDescription
{
    public class GameDescriptionModel
    {
        public string? GameName { get; set; }
        public string? GenreName { get; set; }
        public int? GameId { get; set; }

        public IEnumerable<GamePublisher>? GamePublishers { get; set; }

        public IEnumerable<RegionSale>? RegionSales { get; set; }
    }
}
