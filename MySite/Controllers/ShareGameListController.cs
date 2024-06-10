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
                    ?.Include(e => e.GameLists)
                    ?.ThenInclude(e => e.Games)
                    .FirstOrDefault(g => g.LoginName == userName);

                var userLibList = user.GameLists.FirstOrDefault(gl => gl.NameOfList == "Library");

                string? shareableLink;

                if (userLibList != null )
                { 
                    
                    shareableLink = Url.Action("ShareableGameList", "ShareGameList", new { link = userLibList.ShareableLink }, protocol: HttpContext.Request.Scheme);
                }
                else
                {
                    shareableLink = "No game in your library";
                }
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
         