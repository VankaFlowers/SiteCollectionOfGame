using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;
using MySite.Services;

namespace MySite.Controllers
{
    public class ShareGameListController : Controller
    {
        private readonly DbVideoGamesContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        public ShareGameListController(DbVideoGamesContext context, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }
        [Authorize]
        public IActionResult CreateLink([FromBody] GameListModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                
                var user = _dbContext
                    .Persons
                    .FirstOrDefault(g => g.LoginName == userName);

                UserGameList? gameList;

                if (user.GameLists.Any(gl => gl.Game == "Library"))
                {
                     gameList = user.GameLists.FirstOrDefault(gl => gl.Game == "Library");
                }
                else
                {
                     gameList = new UserGameList()
                    {
                        Person = user,
                        Games = _dbContext
                        .Games
                        .Where(g => model.GamesOfUsers.Select(m => m.Game).ToList().Contains(g.GameName)).ToList(),  //переделать,чтобы не выгружалось в память,а проверялось сразу в базе
                        ShareableLink = Guid.NewGuid(),
                        Game = "Library"
                    };
                    _dbContext.UserGamesList.Add(gameList);
                    _dbContext.SaveChanges();
                }


				var shareableLink = Url.Action("ShareableGameList", "ShareGameList", new { link = gameList.ShareableLink }, protocol: HttpContext.Request.Scheme);
				

				return Json(new {link = shareableLink});
			}
            return BadRequest(ModelState);
        }


        public IActionResult ShareableGameList(Guid link)
        {
            var gameList = _dbContext.UserGamesList
                .Include(g=>g.Games)
                .Include(g=>g.Person)
                .FirstOrDefault(g=> g.ShareableLink == link);
            if(gameList == null)
            {
                return NotFound();
            }
            return View(gameList);
        }
    }
}
         