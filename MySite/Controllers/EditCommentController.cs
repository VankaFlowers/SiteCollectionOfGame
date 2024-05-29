using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;

namespace MySite.Controllers
{
    public class EditCommentController : Controller
    {
        private readonly DbVideoGamesContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EditCommentController(DbVideoGamesContext context, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult EditComment(GamesOfUser game)
        {
            if (ModelState.IsValid)
            {
				if (User.Identity.IsAuthenticated)
				{

					var nameOfPerson = User.Identity.Name;

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
                        .First(e => e.Id==game.CommentId) 
                        .Text = game.Comment;

                    _dbContext.SaveChanges();

                    return View("LibraryHome"); 
                }
                else return View("Index");
            }
            else
            {
                return View("Index");
            }

                
        }
    }
}
