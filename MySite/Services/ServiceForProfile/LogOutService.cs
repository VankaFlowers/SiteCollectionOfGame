using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MySite.Entities;
using MySite.Models;

namespace MySite.Services.ServiceForProfile
{
	public class LogOutService : ILogOutService
	{
		public async Task LogOut(IHttpContextAccessor _httpContextAccessor)
		{
			await _httpContextAccessor.HttpContext.SignOutAsync();
		}
		public async Task<string> EditPassword(IHttpContextAccessor _httpContextAccessor, DbVideoGamesContext _dbVideoGamesContext, EditingPasswordForUserModel model)
		{
			var nameOfUser = _httpContextAccessor.HttpContext.User.Identity.Name;
			var userAsync = _dbVideoGamesContext
				.Persons
				.FirstOrDefaultAsync(p => p.LoginName == nameOfUser);
			var user = await userAsync;

			if ( user.Password == model.OldPassword)
			{
				user.Password = model.NewPassword;
				_dbVideoGamesContext.SaveChanges();
				return "success";
			}
			else
			{
				return "failed";
			}
			
		}
	}
}
