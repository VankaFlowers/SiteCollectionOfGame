using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySite.Entities;
using MySite.Models;
using MySite.Services;
using MySite.Services.ServicesForLibrary;
using System;

namespace MySite.Controllers
{
	[Authorize]
	public class LibraryController : Controller
    {
        private readonly DbVideoGamesContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        public LibraryController(DbVideoGamesContext context, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider; 
        }

        public IActionResult ShowGames()
        {
			if (User.Identity.IsAuthenticated)
			{
                var service = _serviceProvider.GetService<ILibraryService>();

                var gameListModel = service.CreationModel(_dbContext, _httpContextAccessor);

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
				if (User.Identity.IsAuthenticated)
				{
                    var service = _serviceProvider.GetService<ILibraryService>();

                    var gameListModel = service.CreationModel(_dbContext, _httpContextAccessor,game);

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
