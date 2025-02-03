using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TW_System.Models;
using TW_System.Common;

namespace TW_System.Areas.Admin.Controllers
{
	[Area("admin")]
	//[CustomAuthorize]
	public abstract class BaseController<T> : Controller where T : BaseController<T>
	{
		private TWContext _db;

		protected TWContext db => _db ??= HttpContext?.RequestServices.GetService<TWContext>();
	}
}
