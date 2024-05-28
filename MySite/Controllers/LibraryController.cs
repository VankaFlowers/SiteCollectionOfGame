using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;
using System;

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

                var person = _dbContext
                    .Persons
                    .Include(e => e.Games)
                    .ThenInclude(e => e.Genre)
                    .Include(e => e.Comments)
                    .First(p => p.LoginName == nameOfPersonFromCoockie);



                List<GamesOfUser> gamesOfPersonToModel = new List<GamesOfUser>(); //создание списка моделей

                foreach (var game in person.Games)
                {
                    var comment = game?.Comments?.First(e => e.Person == person);

                    if (comment != null)
                    {
                        gamesOfPersonToModel.Add(new GamesOfUser { Game = game.GameName, Comment = comment.Text, CommentId = comment.Id });
                    }
                    else    //если комментария не было
                    {
                        gamesOfPersonToModel.Add(new GamesOfUser { Game = game.GameName, Comment = string.Empty, CommentId = 0 });
                    }


                }
                GameListModel gameListModel = new GameListModel() { GamesOfUsers = gamesOfPersonToModel };
                return View("LibraryHome", gameListModel);
            }
            else
            {
                return View("Index");
            }
        }
        public IActionResult EditComment(GamesOfUser game)
        {
            if (ModelState.IsValid)
            {
                if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("login"))
                {
                    var nameOfPerson = _httpContextAccessor.HttpContext.Request.Cookies["login"];

                    var user = _dbContext
                        .Persons
                        .Include(e => e.Games)
                        .ThenInclude(e => e.Comments)
                        .Include(e => e.Comments)
                        .First(p => p.LoginName == nameOfPerson);

                    user.Comments
                        .First(e => e.Id == game.CommentId)
                        .Text = game.Comment;

                    _dbContext.SaveChanges();

					List<GamesOfUser> gamesOfPersonToModel = new List<GamesOfUser>(); //создание списка моделей

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
					return View("LibraryHome", gameListModel);
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
