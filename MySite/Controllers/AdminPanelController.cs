using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;
using MySite.Services;
using NuGet.LibraryModel;
using System.Runtime.InteropServices;

namespace MySite.Controllers
{
	[Authorize(Roles = "admin")]
	public class AdminPanelController : Controller
	{


		private readonly DbVideoGamesContext _dbContext;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IServiceProvider _serviceProvider;

		public AdminPanelController(DbVideoGamesContext context, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
		{
			_dbContext = context;
			_httpContextAccessor = httpContextAccessor;
			_serviceProvider = serviceProvider;
		}

		public IActionResult Index()
		{
			return View("Index");
		}

		public IActionResult CreatingGameProperties()
		{
			return View();
		}

        public IActionResult CreateGame(CreatingNewGameModel model)
		{
			if (ModelState.IsValid)
			{
				var nameOfNewGame = model.GameName;

				var existGenre = _dbContext.Genres.FirstOrDefault(g => g.GenreName.ToUpper() == nameOfNewGame);
				if (existGenre == null)
				{
					var newGame = new Game()
					{
						GameName = nameOfNewGame
					};
					_dbContext.Add(newGame);
					_dbContext.SaveChanges();
					return View("CreatingGameProperties", new CreatingStatusModel { GameStatus = "Successfully changed" });
				}
				else
				{
					return View("CreatingGameProperties", new CreatingStatusModel { GameStatus = "Something went wrong" });
				}
			}
			else
			{
				return View("CreatingGameProperties", new CreatingStatusModel { GameStatus = "Something went wrong" });
			}
		}


		public IActionResult EditGameName(EditingGameNameModel model)
		{
			var game = _dbContext.Games.FirstOrDefault(g => g.Id == model.GameId);

			var existGame = _dbContext.Games.FirstOrDefault(g => g.GameName.ToUpper() == model.NewGameName.ToUpper());

			CreatingStatusModel status = new CreatingStatusModel();

			AdminEditModel adminEdit = new AdminEditModel();

			if (existGame == null)
			{
				game.GameName = model.NewGameName;

				_dbContext.SaveChanges();
				status.GameStatus = "Successfully changed";
				adminEdit.StatusModel = status;
				adminEdit.GameId = model.GameId;
				adminEdit.GameName = model.NewGameName;
				TempData["GameStatus"] = "Successfully changed";
				return RedirectToAction("GetDescription", "AdminPanel", adminEdit);
			}
			else if (existGame != null)
			{
				status.GameStatus = "Game with such name already exist" ;
			}
			else
			{
				status.GameStatus = "Something went wrong" ;
			}

			return RedirectToAction("GetDescription", "AdminPanel", new AdminEditModel
			{ 
				GameId = model.GameId, GameName = model.CurrentGameName,StatusModel= status 
			});
		}

		public IActionResult DeleteGame(AdminEditModel model)
		{
			if (model.GameName == null && model.GenreName == null)
			{
				return View("DeleteGame");
			}
			if (model.GameName != null)
			{
				var existGame = _dbContext.Games.FirstOrDefault(g => g.GameName == model.GameName);

				if (existGame != null)
				{
					_dbContext.Remove<Game>(existGame);

					_dbContext.SaveChanges();

					model.Text = "Success";

					return View("DeleteGame", model);

				}
			}

			if (model.GenreName != null)            //нужно ли ударять жанр из-за каскадного удаления
			{
				var genre = _dbContext.Genres.FirstOrDefault(g => g.GenreName == g.GenreName);

				if (genre != null)
				{
					_dbContext.Remove<Genre>(genre);

					_dbContext.SaveChanges();
				}
			}
			model.Text = "Success";

			return View("EditGame", model);
		}

		public IActionResult GetGame(AdminEditModel model)
		{
			if (model.GameName == null && model.GenreName == null)
			{
				return View("GetGame");
			}

			var upperGameName = model.GameName.ToUpper();

			var games = _dbContext
				.Games
				.Where(g => g.GameName.ToUpper().Contains(upperGameName));

			model.Games = games.Select(g => g.GameName).ToList();

			return View("GetGame", model);
		}


		public IActionResult GetDescription(AdminEditModel model)
		{
			string gameName = model.GameName;

			var service = _serviceProvider.GetService<ILibraryService>();

			var existGames = service.ShowDescriptionGame(_dbContext, _httpContextAccessor, gameName);

			model.GameDescription = existGames;

			model.StatusModel = new CreatingStatusModel();

			model.StatusModel.GameStatus = TempData["GameStatus"] as string;

			return View("GetDescription", model);
		}


		public IActionResult EditGenreName(EditingGenreNameModel model)
		{
			var genre = _dbContext.Genres.FirstOrDefault(g => g.Id == model.GenreId);

			var existGenre = _dbContext.Genres.FirstOrDefault(g => g.GenreName.ToUpper() == model.NewGenreName.ToUpper());

			var gameName = _dbContext.Games.FirstOrDefault(g => g.Id == model.GameId).GameName;

			CreatingStatusModel status;

			if (existGenre != null)
			{
				genre = existGenre;
				_dbContext.SaveChanges();
			    status = new CreatingStatusModel() { GenreStatus = "Successfully changed" };
			}
			else if (existGenre==null)
			{
				status = new CreatingStatusModel { GenreStatus = "No such genre exists" };
			}
			else
			{
				status = new CreatingStatusModel { GenreStatus = "Something go wrong" };
			}


			return RedirectToAction("GetDescription", "AdminPanel", new AdminEditModel() { GameId = model.GameId, GameName = gameName, StatusModel = status});
		}


		public IActionResult CreateNewGenre(CreatingNewGenreModel model)
		{
			if (ModelState.IsValid)
			{
				var nameOfNewGenre = model.GenreName;

				var existGenre = _dbContext.Genres.FirstOrDefault(g => g.GenreName.ToUpper() == model.GenreName.ToUpper());
				if (existGenre == null)
				{
					var newGenre = new Genre()
					{
						GenreName = model.GenreName
					};
					_dbContext.Add(newGenre);
					_dbContext.SaveChanges();					
					return View("CreatingGameProperties", new CreatingStatusModel { GenreStatus = "Successfully created" });
				}
				else
				{
					return View("CreatingGameProperties", new CreatingStatusModel { GenreStatus = "Such genre already exist" });
				}
			}
			else
			{
				return View("CreatingGameProperties", new CreatingStatusModel { GenreStatus = "Something went wrong" });
			}

		}



	}
}
