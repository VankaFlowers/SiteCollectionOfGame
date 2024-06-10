using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            return "Game already added";
        }
        public string RegularAdding(Person? user, GameOfPerson game, DbVideoGamesContext _dbContext, Game? existGame) //если игры уже есть
        {
            user.Games.Add(existGame);

            var comment = new Comment() { Text = string.Empty, Game = existGame, Person = user };

            if (game.Comment != null)
            {
                comment.Text = game.Comment;
            }
            var userLibList = user.GameLists.FirstOrDefault(gl => gl.NameOfList == "Library");

            userLibList.Games.Add(existGame);

            user.Comments.Add(comment);

            _dbContext.SaveChanges();
            return "Successfully added";
        }
        public string FirstInit(Person? user, Game existGame, GameOfPerson game, DbVideoGamesContext _dbContext) //первая инициализация списков
        {
            user.Games = new List<Game>() { existGame };    //если игр еще не было,для инициалазции                            

            user.Comments = new List<Comment>();

            var userLibList = new UserGameList()
            {
                NameOfList = "Library",
                Person = user,
                Games = new List<Game>() 
                { 
                    existGame 
                }
            };

            user.GameLists = new List<UserGameList>() 
            { 
                userLibList 
            };

            var comment = new Comment() { Text = string.Empty, Game = existGame, Person = user };

            if (game.Comment != null)
            {
                comment.Text = game.Comment;
            }

            user.Comments.Add(comment);

            _dbContext.SaveChanges();

            return "Successfully added";
        }
        public string AddingTheGame(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GameOfPerson game)   //общий метод
        {

            var userContext = _httpContextAccessor.HttpContext.User;

            var nameOfPerson = userContext.Identity.Name;

            var existGame = _dbContext.Games
                    .Where(g => g.GameName == game.NameOfGame)
                    .FirstOrDefault();

            if (existGame == null)  //нет такой игры
            {
                return "Such game not exist";
            }
            var user = _dbContext
            .Persons
            ?.Include(e => e.Games)
            .ThenInclude(e => e.Comments)
            .Include(e => e.Comments)
            .Include(e => e.GameLists)
            .ThenInclude(gl => gl.Games)
            .First(p => p.LoginName == nameOfPerson);

            var alreadyAddedGames = user.Games;

            if (alreadyAddedGames.Count == 0)  //первый раз добавление
            {
                return FirstInit(user, existGame, game, _dbContext);

            }
            else if (alreadyAddedGames.Contains(existGame))    //если игра уже в списке пользователя
            {
                return AlreadyThere(user, game, _dbContext);
            }
            else
            {
                return RegularAdding(user, game, _dbContext, existGame);
            }
        }
        //public GameOfPerson GetGames(DbVideoGamesContext _dbContext,GameOfPerson game)
        //{
        //	var name = game.NameOfGame;			

        //          var games = _dbContext
        //		.Games
        //		.Where(g => g.GameName.ToLower()
        //                            .Contains(name.ToLower()))
        //		.Take(8)
        //		.Select(g => new {id = g.Id, text =g.GameName})
        //		.ToList();

        //	var model = new GameOfPerson 
        //	{ 
        //		Games = new SelectList(games, "Value", "Text")
        //	};
        //	return model;
        //}
    }
}
