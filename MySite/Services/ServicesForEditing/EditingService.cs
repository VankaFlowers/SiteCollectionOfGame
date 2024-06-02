using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;

namespace MySite.Services.ServicesForEditing
{
    public class EditingService : IEditingService
    {
        public void EditComment(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, GameOfUser game)
        {
            var claimsOfUser = _httpContextAccessor.HttpContext.User;

            var nameOfPerson = claimsOfUser.Identity.Name;

            var user = _dbContext
                .Persons
                .Include(e => e.Games)
                .ThenInclude(e => e.Comments)
                .Include(e => e.Comments)
                .First(p => p.LoginName == nameOfPerson);
            if (game.Comment == null)
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
