using Microsoft.AspNetCore.Mvc.Rendering;
using MySite.Entities;
using MySite.Models;
using MySite.Models.GameDescription;

namespace MySite.Services
{
    interface IAddingGameService
    {
        string AlreadyThere(Person? user, GameOfPerson game, DbVideoGamesContext _dbContext) ;
        string RegularAdding(Person? user, GameOfPerson game, DbVideoGamesContext _dbContext, Game? existGame) ;
        string FirstInit(Person? user, Game existGame, GameOfPerson game, DbVideoGamesContext _dbContext) ;
        string AddingTheGame(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GameOfPerson game);
        //public GameOfPerson GetGames(DbVideoGamesContext _dbContext, GameOfPerson game)
    }
    interface ILibraryService
    {
        GameListModel CreationModel(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GameOfUser game = null);
        void EditComment(Person? user, GameOfUser game, DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor);

        public GameDescriptionModel ShowDescriptionGame(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, string gameName);

	}
    interface IHomeService
    {
        string Registring(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, Log log);
        string Logging(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, Log log);
    }
    interface IEditingService
    {
        void EditComment(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GameOfUser game);
    }
}
