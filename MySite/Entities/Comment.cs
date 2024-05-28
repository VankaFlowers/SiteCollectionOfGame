namespace MySite.Entities
{
    public class Comment : ContextBoundObject
    {
        public int Id { get; set; }
        public string Text {  get; set; }

        public Person Person { get; set; }

        public Game Game { get; set; }
    }
}
