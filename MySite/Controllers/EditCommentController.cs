using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;
using MySite.Services;

namespace MySite.Controllers
{
    public class EditCommentController : Controller
    {
        private readonly DbVideoGamesContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        public EditCommentController(DbVideoGamesContext context, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }
        public IActionResult EditComment(GamesOfUser game)
        {
            if (ModelState.IsValid)
            {
				if (User.Identity.IsAuthenticated)
				{
                    var service = _serviceProvider.GetRequiredService<IEditingService>();

                    service.EditComment(_dbContext, _httpContextAccessor, game);

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
