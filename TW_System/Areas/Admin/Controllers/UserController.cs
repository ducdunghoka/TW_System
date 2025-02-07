using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TW.Common;
using TW_System.Models;

namespace TW_System.Areas.Admin.Controllers
{
	public class UserController : BaseController<UserController>
	{
		public IActionResult Index()
		{
			return RedirectToAction("login", "home");
		}

		[AllowAnonymous, Route("login")]
		public IActionResult Login()
		{
			return View();
		}

		[AllowAnonymous, Route("login")]
		[HttpPost]
		public async Task<IActionResult> Login(string username, string password)
		{
			var account = db.TW_Users.FirstOrDefault(x => x.Account == username && x.Password == password);

			if (account == null)
			{
				TempData["LoginMessage"] = "Tài khoản hoặc mật khẩu không đúng";
				return RedirectToAction("login", "account");
			}

			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name, username.ToUpper())
			};

			ClaimsIdentity identity = new(claims, AuthenticationTypes.ApplicationCookie);
			ClaimsPrincipal principal = new(identity);
			await HttpContext.SignInAsync(
					scheme: AuthenticationTypes.ApplicationCookie,
					principal: principal,
					properties: new AuthenticationProperties
					{
						IsPersistent = true,
					}
				);
			return RedirectToAction("index", "home");
		}

		[AllowAnonymous, Route("signUp")]
		public IActionResult SignUp()
		{
			return View();
		}

		[AllowAnonymous, Route("signUp")]
		[HttpPost]
		public async Task<IActionResult> SignUp(string username, string email, string fullname,string phone, string address, string password, string confirmPassword)
		{
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
			{
				TempData["SignUpMessage"] = "Cần điền đủ thông tin";
				return RedirectToAction("signUp", "user");
			}
			if (password != confirmPassword)
			{
				TempData["SignUpMessage"] = "Mật khẩu không trùng nhau";
				return RedirectToAction("signUp", "user");
			}
			if (phone.Length < 9 || phone.Length > 10 || !phone.StartsWith("0"))
			{
				TempData["SignUpMessage"] = $"Số điện thoại không hợp lệ {phone}";
				return RedirectToAction("signUp", "user");
			}
			if (db == null)
			{
				return BadRequest("null");
			}
			if (db.TW_Users == null)
			{
				return BadRequest("null");
			}
			var account = db.TW_Users.FirstOrDefault(x => x.Account.Trim() == username.Trim() && x.Email.Trim() == email.Trim() && x.Phone.Trim() == phone.Trim() && x.Address.Trim() == address.Trim());
			if (account != null)
			{
				TempData["SignUpMessage"] = "Thông tin tài khoản đã tồn tại";
				return RedirectToAction("signUp", "user");
			}
			var maxCode = db.TW_Users.Max(x => x.No);
			int maxCodeInt = 0;
			if (!string.IsNullOrEmpty(maxCode))
			{
				maxCodeInt = int.Parse(maxCode);
			}
			var newCode = (maxCodeInt + 1).ToString("D4");
			account = new TW_User()
			{
				Account = username,
				No = newCode,
				FullName= fullname,
				Email = email,
				Phone = phone,
				Address = address,
				Password = password,
				Role = RoleType.User,
				CreatedAt = DateTime.Now,
			};
			db.TW_Users.Add(account);
			await db.SaveChangesAsync();
			TempData["LoginMessage"] = "Đăng ký tài khoản thành công";
			return RedirectToAction("Login", "User");
		}
	}
}
