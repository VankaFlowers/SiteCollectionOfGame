using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;
using MySite.Services.ServicesForSelection;

namespace MySite.Controllers
{
	[Authorize]
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
				if (User.Identity.IsAuthenticated)
				{
					var nameOfPerson = User.Identity.Name; //_httpContextAccessor.HttpContext.Request.Cookies["login"];   //получаем из куки имя пользователя

					AddingGame adding = new AddingGame();

					string nameOfView = adding.AddingTheGame(_dbContext, _httpContextAccessor, game);

					return View(nameOfView);
				}
				else return View("Index");  //если нет куки

			}

			return View("FailedGameSelection");
		}


	}


}
