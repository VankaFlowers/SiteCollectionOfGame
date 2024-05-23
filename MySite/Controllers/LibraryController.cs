using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;

namespace MySite.Controllers
{
    public class LibraryController : Controller
    {
        private readonly DbVideoGamesContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LibraryController(DbVideoGamesContext context, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public IActionResult ShowGames()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("login"))
            {
                var nameOfPersonFromCoockie = _httpContextAccessor.HttpContext.Request.Cookies["login"];

                var gamesOfPerson = _dbContext
                    .Persons  //так же подключить другие таблицы для доп отображения
                    .Include(e => e.Games)
                    .ThenInclude(e => e.Genre)
                    .First(p => p.LoginName == nameOfPersonFromCoockie)                    
                    ?.Games
                    .Select(g => g.GameName);
                List<GamesOfUser> gamesOfPersonToModel = new List<GamesOfUser>(); //создание списка моделей
                foreach (var game in gamesOfPerson)
                {
                    gamesOfPersonToModel.Add(new GamesOfUser { Game = game });
                }
                              
                return View("LibraryHome", gamesOfPersonToModel);
            }
            else
            {
                return View("Index");
            }
        }
    }
}
