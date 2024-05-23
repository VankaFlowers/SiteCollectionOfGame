using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;

namespace MySite.Controllers
{
    public class SelectionController : Controller
    {
        private readonly DbVideoGamesContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SelectionController(DbVideoGamesContext context, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;
            //_httpContext = httpContext;
        }
        [HttpGet]
        public IActionResult GameSelection()
        {
            return View("GameSelection");
        }
        [HttpPost]
        public IActionResult AddGame(GameOfPerson game) {
            if (ModelState.IsValid)
            {
                var existGame = _dbContext.Games
                    .Where(g => g.GameName == game.NameOfGame )
                    .FirstOrDefault();
                if (existGame == null)
                {
                    return View("FailedGameSelection");
                }
                else
                {
                    if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("login"))   //получаем из куки имя пользователя
                    {
                        var nameOfPerson = _httpContextAccessor.HttpContext.Request.Cookies["login"];

                        var user = _dbContext 
                            .Persons
                            .Include(e => e.Games)
                            .Where(p => p.LoginName == nameOfPerson)
                            .FirstOrDefault();

                        var alreadyAddedGames = user.Games;
                        
                        
                        if (alreadyAddedGames == null)
                        {
                            //alreadyAddedGames = new List<Game>() { existGame };
                            user.Games = new List<Game>() { existGame };
                            //_dbContext.Update(user);
                            _dbContext.SaveChanges();
                            return View("SuccessGameSelection");
                        }
                        else if (alreadyAddedGames.Contains(existGame) == true  )       //если игра уже в списке пользователя
                        {
                            return View("SuccessGameSelection");
                        }
                        else
                        {
                            //alreadyAddedGames.Add(existGame);
                            user.Games.Add(existGame);
                            //_dbContext.Update(user);
                            _dbContext.SaveChanges();
                        }
                        
                    }
                    else return View("Index");  //если нет куки
                    
                    return View("SuccessGameSelection");                 
                }
            }
            
            return View("FailedGameSelection");
        }
        
        
    }

       
}
