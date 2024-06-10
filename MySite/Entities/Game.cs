using System;
using System.Collections.Generic;

namespace MySite.Entities;

public partial class Game
{
    public int Id { get; set; }

    public int? GenreId { get; set; }

    public string? GameName { get; set; }

    public virtual ICollection<GamePublisher> GamePublishers { get; set; } = new List<GamePublisher>();

    public virtual Genre? Genre { get; set; }

    public ICollection<Comment>? Comments{ get; set; }

    public ICollection<Person>? Persons { get; set; }

    public ICollection<UserGameList>? GameLists { get; set; }
}
