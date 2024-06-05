using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;
using System.Security.Claims;

namespace MySite.Services.ServicesForHome
{
    public class HomeService : IHomeService
    {
        public string Logging(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, Log log)
        {
            var alreadyExist = _dbContext.Persons   //проверка пользователя
            .Where(p => (p.LoginName == log.Email && p.Password == log.Password) ? true : false)
            .FirstOrDefault();

            

            if (alreadyExist == null) //логика если неправильно
            {
                return "FailedLogin";
            }
            else  //если правильно
            {
                if (alreadyExist.Role == null) //временно,чтобы существующим проставить роли
                {
                    alreadyExist.Role = "user";
                    _dbContext.SaveChanges();
                }

                var claims = new List<Claim> 
                { 
                    new Claim(ClaimTypes.Name, alreadyExist.LoginName),

                    new Claim(ClaimTypes.Role, alreadyExist.Role)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                // установка аутентификационных куки
                _httpContextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return "Profile";
            }
        }
        public string Registring(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, Log log)
        {
            var alreadyExist = _dbContext.Persons
                    .Where(p => p.LoginName == log.Email ? true : false)
                    .FirstOrDefault();

            if (alreadyExist == null)
            {
                var login = new Person()
                {
                    LoginName = log.Email,
                    Password = log.Password,
                    Role = "user"
                };
                _dbContext.Add(login);
                _dbContext.SaveChanges();
                return "Index";
            }
            else  //логика если уже есть
            {
                return "Index";
            }
        }
    }
}
