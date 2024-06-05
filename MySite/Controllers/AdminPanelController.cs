using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;
using MySite.Services;
using NuGet.LibraryModel;

namespace MySite.Controllers
{
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


		public IActionResult CreateGame(AdminEditModel model)
		{
			if (model.GameName == null && model.GenreName == null)
			{
				return View("CreateGame");
			}

			if (model.GenreName != null)
			{
				var existGenre = _dbContext.Genres.FirstOrDefault(g => g.GenreName == model.GenreName);
				if (existGenre == null)
				{       //если такого жанра нет,добавляем новый
					var newGenre = new Genre()
					{
						GenreName = model.GenreName
					};
					_dbContext.Add(newGenre);
					_dbContext.SaveChanges();
				}
			}
			var existGame = _dbContext.Games.FirstOrDefaultAsync(g => g.GameName == model.GameName);

   //         if ()
			//{

			//}
			var newGame = new Game()
			{
				GameName = model.GameName,
				Genre = _dbContext.Genres.FirstOrDefault(g => g.GenreName == model.GenreName)
			};
			_dbContext.Add(newGame);
			_dbContext.SaveChanges();

			model.Text = "Success";

			return View("CreateGame",model);
		}


		public IActionResult EditGame(AdminEditModel model)
		{
            if (model.GameName == null && model.GenreName == null)
            {
                return View("EditGame");
            }

            if (model.GameName != null)
			{
				var existGame = _dbContext.Games.FirstOrDefault(g => g.GameName == model.GameName);

				existGame.GameName = model.GameName;

				_dbContext.SaveChanges();
			}

			if (model.GenreName != null)
			{
				var genre = _dbContext.Genres.FirstOrDefault(g => g.GenreName == g.GenreName);

				genre.GenreName = model.GenreName;

				_dbContext.SaveChanges();
			}

            model.Text = "Success";

            return View("EditGame", model);
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

			if (model.GenreName != null)			//нужно ли ударять жанр из-за каскадного удаления
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

			var existGames = service.ShowDescriptionGame(_dbContext,_httpContextAccessor, gameName);

			model.GameDescription = existGames;

			return View("GetDescription", model);
		}



	}
}
