using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;

namespace MySite.Services.ServicesForSelection
{
	public class AddingGame : IAddingGameService
    {
		  public string AlreadyThere(Person? user, GameOfPerson game, DbVideoGamesContext _dbContext)
		{
			
			//var comment = new Comment() { Text = game.Comment };
			//user.Comments = new List<Comment>() { comment };
			//_dbContext.SaveChanges();
			return "SuccessGameSelection";
		}
		 public string RegularAdding(Person? user, GameOfPerson game, DbVideoGamesContext _dbContext, Game? existGame)
		{
			user.Games.Add(existGame);

			var comment = new Comment() { Text = string.Empty, Game = existGame, Person = user };

			if (game.Comment != null)
			{
				comment.Text = game.Comment;
			}

			user.Comments.Add(comment);

			_dbContext.SaveChanges();
			return "SuccessGameSelection";
		}
		 public string FirstInit(Person? user,Game existGame, GameOfPerson game, DbVideoGamesContext _dbContext)
		{
			user.Games = new List<Game>() { existGame };    //если игр еще не было,для инициалазции                            

			user.Comments = new List<Comment>();

			var comment = new Comment() { Text = string.Empty, Game = existGame, Person = user };

			if (game.Comment != null)
			{
				comment.Text = game.Comment;
			}

			user.Comments.Add(comment);

			_dbContext.SaveChanges();

			return "SuccessGameSelection";
		}
		   public string AddingTheGame(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GameOfPerson game)	//общий метод
		{

			var userContext = _httpContextAccessor.HttpContext.User;

			var nameOfPerson = userContext.Identity.Name; 

			var existGame = _dbContext.Games
					.Where(g => g.GameName == game.NameOfGame)
					.FirstOrDefault();

			if (existGame == null)	//нет такой игры
			{
				return "FailedGameSelection";
			}
			var user = _dbContext
			.Persons
			?.Include(e => e.Games)
			.ThenInclude(e => e.Comments)
			.Include(e => e.Comments)
			.First(p => p.LoginName == nameOfPerson);

			var alreadyAddedGames = user.Games;    

			if (alreadyAddedGames == null)	//первый раз добавление
			{
				return FirstInit(user, existGame, game, _dbContext);
				
			}
			else if (alreadyAddedGames.Contains(existGame))    //если игра уже в списке пользователя
			{				
				return AlreadyThere(user,game,_dbContext);
			}
			else
			{
				return RegularAdding(user, game, _dbContext,existGame);
			}
		}
	}
}
