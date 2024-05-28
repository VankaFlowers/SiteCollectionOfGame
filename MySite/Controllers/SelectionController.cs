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
        public IActionResult AddGame(GameOfPerson game)
        {
            if (ModelState.IsValid)
            {
                var existGame = _dbContext.Games
                    .Where(g => g.GameName == game.NameOfGame)
                    .FirstOrDefault();
                if (existGame == null)
                {
                    return View("FailedGameSelection");
                }
                else
                {
                    if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("login"))   
                    {
                        var nameOfPerson = _httpContextAccessor.HttpContext.Request.Cookies["login"];   //получаем из куки имя пользователя


                        var user = _dbContext
                        .Persons
                        .Include(e => e.Games)
                        .ThenInclude(e => e.Comments)
                        .Include(e => e.Comments)
                        .First(p => p.LoginName == nameOfPerson);

                        var alreadyAddedGames = user.Games; //уже добавленные игры пользователя   


                        if (alreadyAddedGames == null)
                        {
                            user.Games = new List<Game>() { existGame };    //если игр еще не было,для инициалазции                            

                            user.Comments = new List<Comment>();

                            var comment = new Comment() { Text = string.Empty, Game = existGame, Person = user };

                            if (game.Comment != null)
                            {
                                comment.Text = game.Comment;
                            }                            

                            user.Comments.Add(comment);            
                            
                            _dbContext.SaveChanges();

                            return View("SuccessGameSelection");
                        }
                        else if (alreadyAddedGames.Contains(existGame) )    //если игра уже в списке пользователя
                        {
                            var comment = new Comment() { Text = game.Comment };

                            user.Comments = new List<Comment>() { comment };

                            _dbContext.SaveChanges();
                            return View("SuccessGameSelection");
                        }
                        else
                        {//здесь поправить null ex
                            user.Games.Add(existGame);

                            var comment = new Comment() { Text = string.Empty, Game = existGame, Person = user };

                            if (game.Comment != null)
                            {
                                comment.Text = game.Comment;
                            }

                            user.Comments.Add(comment);
                            
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
