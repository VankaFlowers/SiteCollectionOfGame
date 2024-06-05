using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;
using MySite.Models.GameDescription;

namespace MySite.Services.ServicesForLibrary
{
    public class LibraryService : ILibraryService
    {
        public GameListModel CreationModel(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GameOfUser game = null)
        {
            var claimsOfUser = _httpContextAccessor.HttpContext.User;

            var nameOfPerson = claimsOfUser.Identity.Name;

            var user = _dbContext
                .Persons
                .AsQueryable()
                ?.Include(e => e.Games)
                .ThenInclude(e => e.Comments)
                .Include(e => e.Comments)
                .First(p => p.LoginName == nameOfPerson);

            List<GameOfUser> gamesOfPersonToModel = new List<GameOfUser>();
            if (game != null)
            {
                EditComment(user, game, _dbContext, _httpContextAccessor);
            }
            foreach (var gameOfUser in user.Games)
            {
                var comment = gameOfUser?.Comments?.First(e => e.Person == user);

                if (comment != null)
                {
                    gamesOfPersonToModel.Add(new GameOfUser { Game = gameOfUser.GameName, Comment = comment.Text, CommentId = comment.Id });
                }
                else    //если комментария не было
                {
                    gamesOfPersonToModel.Add(new GameOfUser { Game = gameOfUser.GameName, Comment = string.Empty, CommentId = 0 });
                }
            }
            GameListModel gameListModel = new GameListModel() { GamesOfUsers = gamesOfPersonToModel };

            return gameListModel;
        }


        public void EditComment(Person? user, GameOfUser game, DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor)
        {
            if (game.Comment == null)  //если пользватель удалил комментарий
            {
                game.Comment = string.Empty;
            }
            user.Comments
                .First(e => e.Id == game.CommentId)
                .Text = game.Comment;

            _dbContext.SaveChanges();
        }


        public GameDescriptionModel ShowDescriptionGame(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, string gameName)
        {
            //var claimsOfUser = _httpContextAccessor.HttpContext.User;

            //var nameOfPerson = claimsOfUser.Identity.Name;

            var genre = _dbContext.Games
                .Include(g=>g.Genre)
                 .FirstOrDefault(g => g.GameName == gameName)
                 ?.Genre
                 ?.GenreName;
            if (genre == null)
            {
                genre = string.Empty;
            }

            var gamePublishers = _dbContext
                 .GamePublishers
                 .Include(gp => gp.Game)
                 .Include(gp => gp.Publisher)
                 .Include(gp => gp.GamePlatforms)
                 .ThenInclude(e => e.Platform)
                 .Where(gp => gp.Game.GameName == gameName);

            var sales = _dbContext
                .RegionSales
                .Include(rg => rg.Region);


            //?.Include(e => e.GamePublishers)
            //?.ThenInclude(e => e.Publisher)

            //            //
            //?.ThenInclude(e => e.GamePublishers)
            //            ?.ThenInclude(e => e.GamePlatforms)
            //?.ThenInclude(e => e.Platform)
            //?.FirstOrDefault(g => g.GameName == gameName)
            //            ?.GamePublishers;




            var descriptionGame = new GameDescriptionModel()
            {
                GameName = gameName,
                GenreName = genre,
                GamePublishers = gamePublishers.ToList(),
                RegionSales = sales.ToList()
            };
            return descriptionGame;
        }
    }
}
