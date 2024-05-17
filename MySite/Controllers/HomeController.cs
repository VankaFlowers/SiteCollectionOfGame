using Microsoft.AspNetCore.Mvc;
using MySite.Models;
using System.Diagnostics;



namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        
        public IActionResult Logging(Log log)
        {
            if (ModelState.IsValid)
            {
                using (var logContext = new Entities.DbVideoGamesContext())
                {
                    var alreadyExist = logContext.Persons
                        .Where(p => 
                        (p.LoginName == log.Email && p.Password==log.Password) ? true : false) //������ �� ������������ ������ � ������
                        .FirstOrDefault();                      
                    
                    if (alreadyExist == null ) //������ ���� �����������
                    {
                        
                    }
                    else  //���� ���������
                    {
                        
                    }
                    

                }
                return Redirect("/"); //���������� �� ������� �������� 
            }
            return View("Index");
        }
        public IActionResult Register(Log log)
        {
            if (ModelState.IsValid)
            {
                using (var logContext = new Entities.DbVideoGamesContext())
                {
                    var alreadyExist = logContext.Persons
                        .Where(p => p.LoginName == log.Email ? true : false)
                        .FirstOrDefault();

                    if (alreadyExist == null) //
                    {
                        var login = new Entities.Person()
                        {
                            LoginName = log.Email,
                            Password = log.Password
                        };
                        logContext.Add(login);
                        logContext.SaveChanges();
                    }
                    else  //������ ���� ��� ����
                    {

                    }


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
