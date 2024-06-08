
namespace MySite.Entities
{
    
    public partial class Person : ContextBoundObject
    {        
        public int Id { get; set; }

        public string? LoginName { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }
        public string? Enable2FA { get; set; }
        public string? Code2FA {  get; set; }

        public ICollection<Game>? Games{ get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<UserGameList>? GameLists { get; set; }
    }
}
