using MySite.Entities;
using MySite.Models;

namespace MySite.Services
{
    interface IAddingGameService
    {
        string AlreadyThere(Person? user, GameOfPerson game, DbVideoGamesContext _dbContext) ;
        string RegularAdding(Person? user, GameOfPerson game, DbVideoGamesContext _dbContext, Game? existGame) ;
        string FirstInit(Person? user, Game existGame, GameOfPerson game, DbVideoGamesContext _dbContext) ;
        string AddingTheGame(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GameOfPerson game);
    }
    interface ILibraryService
    {
        GameListModel CreationModel(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GamesOfUser game = null);
        void EditComment(Person? user, GamesOfUser game, DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor);        
    }
    interface IHomeService
    {
        string Registring(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, Log log);
        string Logging(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, Log log);
    }
    interface IEditingService
    {
        void EditComment(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GamesOfUser game);
    }
}
