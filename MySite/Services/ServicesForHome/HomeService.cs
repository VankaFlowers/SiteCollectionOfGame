using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;
using System.Net.Mail;
using System.Security.Claims;

namespace MySite.Services.ServicesForHome
{
    public class HomeService : IHomeService
    {
        public async Task<string> Logging(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, Log log, IServiceProvider serviceProvider)
        {
            var alreadyExist = _dbContext.Persons   //проверка пользователя
            .Where(p => (p.LoginName == log.Email && p.Password == log.Password))
            .FirstOrDefault();

            if (alreadyExist == null) //логика если неправильно
            {
                return "FailedLogin";
            }
            if (alreadyExist.Enable2FA == "email")    //если есть двойная аутентификация
            {
                return await LoggingFor2FAUsers(_dbContext, _httpContextAccessor, log, serviceProvider,alreadyExist);
            }
            else  //если правильно
            {
                if (alreadyExist.Role == null) //временно,чтобы существующим проставить роли
                {
                    alreadyExist.Role = "user";
                    _dbContext.SaveChanges();
                }
                await SettingAuthCookies(alreadyExist, _httpContextAccessor);
                return "Profile";
            }
        }
        public async Task<string> LoggingFor2FAUsers(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, Log log, IServiceProvider _serviceProvider, Person? alreadyExist)
        {
            var emailSevice = _serviceProvider.GetRequiredService<IEmailService>();
            var code = GenerateTwoFactorCode();
            alreadyExist.Code2FA = code;
            _dbContext.SaveChanges();
            await emailSevice.SendEmailAsync(alreadyExist.LoginName, "verification code", $"Your authentication code is: {code}");
            return "DoubleAuthFormLogging";
        }

        public async Task<string> VerifyCode(DbVideoGamesContext _dbContext, IHttpContextAccessor _httpContextAccessor, Log log )
        {
            var alreadyExist = _dbContext.Persons
            .Where(p => p.LoginName == log.Email)
            .FirstOrDefault();
            if (log.Code == alreadyExist.Code2FA)
            {
                await SettingAuthCookies(alreadyExist, _httpContextAccessor);
                return "Profile";
            }
            else
            {
                return "FailedLogin";
            }
        }


        public async Task SettingAuthCookies(Person? alreadyExist, IHttpContextAccessor _httpContextAccessor)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, alreadyExist.LoginName),

                    new Claim(ClaimTypes.Role, alreadyExist.Role)
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            // установка аутентификационных куки
            await _httpContextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
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
                    Role = "user",
                };
                if (log.Enable2FA == true)
                {
                    login.Enable2FA = "email";
                }
                _dbContext.Add(login);
                _dbContext.SaveChanges();
                return "Index";
            }
            else  //логика если уже есть
            {
                return "Index";
            }
        }

        private string GenerateTwoFactorCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }


    }
}
