using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;

namespace MySite.Services.ServicesForLibrary
{
    public class LibraryService : ILibraryService
    {
         public GameListModel CreationModel( DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GamesOfUser game = null)
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

            List<GamesOfUser> gamesOfPersonToModel = new List<GamesOfUser>();
            if (game != null)
            {
                EditComment(user, game, _dbContext, _httpContextAccessor);
            }
            foreach (var gameOfUser in user.Games)
            {
                var comment = gameOfUser?.Comments?.First(e => e.Person == user);

                if (comment != null)
                {
                    gamesOfPersonToModel.Add(new GamesOfUser { Game = gameOfUser.GameName, Comment = comment.Text, CommentId = comment.Id });
                }
                else    //если комментария не было
                {
                    gamesOfPersonToModel.Add(new GamesOfUser { Game = gameOfUser.GameName, Comment = string.Empty, CommentId = 0 });
                }
            }
            GameListModel gameListModel = new GameListModel() { GamesOfUsers = gamesOfPersonToModel };

            return gameListModel;
        }


          public void EditComment(Person? user,GamesOfUser game, DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor)
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
    }
}
