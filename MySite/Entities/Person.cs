
namespace MySite.Entities
{
    
    public partial class Person : ContextBoundObject
    {        
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public ICollection<Game>? Games { get; set; }
    }
}
