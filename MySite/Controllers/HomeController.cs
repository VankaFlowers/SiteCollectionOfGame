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
using MySite.Services;
using System.Net.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbVideoGamesContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        public HomeController(ILogger<HomeController> logger, DbVideoGamesContext context, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Logging(Log log)
        {
            if (ModelState.IsValid)
            {
                var service = _serviceProvider.GetService<IHomeService>();

                var nameOfView = service.Logging(_dbContext, _httpContextAccessor, log, _serviceProvider);

                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Index", "AdminPanel");
                }
                TempData["logEmail"] = log.Email;
                TempData["password"] = log.Password;
                return View(await nameOfView);
            }
            return View("Index");
        }

        public async Task<IActionResult> VerifyCode(Log log)
        {
                var service = _serviceProvider.GetService<IHomeService>();
                
                var nameOfView = service.VerifyCode(_dbContext,_httpContextAccessor, log);

                return View(await nameOfView);         
        }

        [HttpPost]
        public IActionResult Registring(Log log)
        {
            if (ModelState.IsValid)
            {
                var service = _serviceProvider.GetService<IHomeService>();

                var nameOfView = service.Registring(_dbContext, _httpContextAccessor, log);

                return View(nameOfView);
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
