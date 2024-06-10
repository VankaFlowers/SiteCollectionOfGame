using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySite.Entities;
using MySite.Models;
using MySite.Services;

namespace MySite.Controllers
{
	[Authorize]
	public class ProfileController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly DbVideoGamesContext _dbContext;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IServiceProvider _serviceProvider;
		public ProfileController(ILogger<HomeController> logger, DbVideoGamesContext context, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
		{
			_logger = logger;
			_dbContext = context;
			_httpContextAccessor = httpContextAccessor;
			_serviceProvider = serviceProvider;
		}

		public async Task<IActionResult> Index()
		{
			return View("Index");
		}


		public async Task<IActionResult> LogOut()
		{
			var service = _serviceProvider.GetRequiredService<ILogOutService>();

			await service.LogOut(_httpContextAccessor);

			return RedirectToAction("Index", "Home");
		}
		
		public async Task<IActionResult> EditPassword(EditingPasswordForUserModel model)
		{
			var service = _serviceProvider.GetRequiredService<ILogOutService>();

			var status = await service.EditPassword(_httpContextAccessor,_dbContext, model);
			if(status == "success")
			{
				TempData["status"] = "Password changed successfully";
			}
			else
			{
				TempData["status"] = "The current password is incorrect";
			}
			return View("Index");
		}
	}
}
