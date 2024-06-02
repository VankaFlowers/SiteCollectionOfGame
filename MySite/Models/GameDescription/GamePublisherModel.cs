namespace MySite.Models.GameDescription
{
    public class GamePublisherModel
    {
        public string GamePublisherName { get; set; }
        public ICollection<GamePlatformModel> GamePlatformModels {get;set;}
    }
}
