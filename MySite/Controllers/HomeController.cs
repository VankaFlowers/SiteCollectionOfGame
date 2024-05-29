using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MySite.Models;
using System.Diagnostics;
using System.Security.Claims;
using RestSharp;
using MySite.Entities;
using System;
using Microsoft.AspNetCore.Http;




namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbVideoGamesContext _dbContext;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HomeController(ILogger<HomeController> logger, DbVideoGamesContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _dbContext = context;
			_httpContextAccessor = httpContextAccessor;
		}

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RedirectToSelection()
        {
            return RedirectToRoute(new { controller = "Selection", action = "GameSelection" });
        }
        public IActionResult RedirectToLibrary()
        {
            return RedirectToRoute(new { controller = "Library", action = "ShowGames" });
        }
        [HttpPost]

        public IActionResult Logging(Log log)
        {
            if (ModelState.IsValid)
            {
				var alreadyExist = _dbContext.Persons
                    .Where(p =>
                    (p.LoginName == log.Email && p.Password == log.Password) ? true : false) //������ �� ������������ ������ � ������
                    .FirstOrDefault();

				

				if (alreadyExist == null) //������ ���� �����������
                {
                    return View("FailedLogin");
                }
                else  //���� ���������
                {
					var claims = new List<Claim> { new Claim(ClaimTypes.Name, alreadyExist.LoginName) };

					ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
					// ��������� ������������������ ����
					_httpContextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
					return View("Profile");
                }

                return Redirect("/"); //���������� �� ������� �������� 
            }
            return View("Index");
        }
        [HttpPost]
        public IActionResult Registring(Log log)
        {
            if (ModelState.IsValid)
            {
                var alreadyExist = _dbContext.Persons
                    .Where(p => p.LoginName == log.Email ? true : false)
                    .FirstOrDefault();

                if (alreadyExist == null) //
                {
                    var login = new Entities.Person()
                    {
                        LoginName = log.Email,
                        Password = log.Password
                    };
                    _dbContext.Add(login);
                    _dbContext.SaveChanges();
                }
                else  //������ ���� ��� ����
                {

                }
                return Redirect("/"); //���������� �� ������� �������� 
            }
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
