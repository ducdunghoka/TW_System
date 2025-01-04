
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TW.Models;

namespace TW.Common
{
	public interface IMySessions
	{
		string EmpId { get; }
		string FullName { get; }
		string Factory { get; }
		//TW_User No { get; }
		//bool ControlAccess(string name, string method);
		void SetPayslipMaster();
		bool GetPayslipMaster();
		void SetPayslipMember(string value);
		bool GetPayslipMember();
	}
	public class MySessions : IMySessions
	{
		private readonly ISession session;
		private readonly IHttpContextAccessor context;
		private readonly string project;
		private readonly IMemoryCache _cache;
		public MySessions(IHttpContextAccessor httpContextAccessor, IConfiguration config, IMemoryCache cache)
		{
			_cache = cache;
			session = httpContextAccessor.HttpContext.Session;
			context = httpContextAccessor;
			project = config.GetValue<string>("Project");
		}

		public string EmpId
		{
			get
			{
				return context.HttpContext.User.Identity.Name;
			}
		}

		public void SetPayslipMaster()
		{
			session.SetString("SS-Payslip-Master", "Yes");
		}

		public bool GetPayslipMaster()
		{
			var result = session.GetString("SS-Payslip-Master");
			return result == "Yes";
		}

		public void SetPayslipMember(string value)
		{
			session.SetString("SS-Payslip-Member", value);
		}

		public bool GetPayslipMember()
		{
			var result = session.GetString("SS-Payslip-Member");
			return result == "Yes";
		}

		//public TW_User No
		//{
		//	get
		//	{
		//		int emp = 5;
		//		var result = session.GetString("SS-UserInfo");
		//		if (!string.IsNullOrEmpty(result))
		//		{
		//			return JsonConvert.DeserializeObject<Employee>(result);
		//		}
		//		var empid = context.HttpContext.User.Identity.Name;
		//		var emp = _user.Employees.FirstOrDefault(x => x.EmpId == context.HttpContext.User.Identity.Name) ?? new Employee();
		//		session.SetString("SS-UserInfo", JsonConvert.SerializeObject(emp));
		//		return emp;
		//	}
		//}

		public string FullName
		{
			get
			{
				//var name = session.GetString("FullName");
				//var empid = session.GetString("EmpId");

				//if (string.IsNullOrEmpty(name) || empid != context.HttpContext.User.Identity.Name)
				//{
				//	var emp = _user.Employees.FirstOrDefault(x => x.EmpId == context.HttpContext.User.Identity.Name) ?? new Employee();
				//	session.SetString("FullName", emp.EmpName);
				//	session.SetString("EmpId", emp.EmpId);
				//	name = emp.EmpName;
				//}
				return "name";
			}
		}

		public string Factory
		{
			get
			{
				//var name = session.GetString("Factory");
				//var empid = session.GetString("EmpId");

				//if (string.IsNullOrEmpty(name) || empid != context.HttpContext.User.Identity.Name)
				//{
				//	var emp = _user.Employees.FirstOrDefault(x => x.EmpId == context.HttpContext.User.Identity.Name) ?? new Employee();
				//	string factory = emp.Factory;
				//	if (factory == "GFT")
				//	{
				//		factory = emp.LineNo.EndsWith("-A") ? "VNA" : emp.LineNo.EndsWith("-B") ? "VNB"
				//		: emp.LineNo.EndsWith("-C") ? "VNC"
				//		: emp.LineNo.EndsWith("-D") ? "VND"
				//		: emp.LineNo.EndsWith("-E") ? "VNE"
				//		: emp.LineNo.EndsWith("-H") ? "VNH" : emp.Factory;
				//	}

				//	session.SetString("Factory", factory);
				//	session.SetString("EmpId", emp.EmpId);
				//	name = factory;
				//}
				return "name";
			}
		}

		//public bool ControlAccess(string name, string method)
		//{
		//	string empid = context.HttpContext.User.Identity.Name;
		//	var roleIds = _user.CoreEmployees.Where(x => x.EmpId == empid && x.Project == project).Select(x => x.RoleId).ToList();
		//	var publishRole = _user.CoreRoles.FirstOrDefault(x => x.Name == "Publish" && x.Project == project);

		//	if (publishRole != null && roleIds.Contains(publishRole.Id) == false)
		//	{
		//		roleIds.Add(publishRole.Id);
		//	}

		//	if (roleIds.Count == 0)
		//	{
		//		return false;
		//	}

		//	if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(method))
		//	{
		//		return true;
		//	}

		//	var data = (from m in _user.CoreAuthorizeMaps
		//				join c in _user.CoreAuthorizes on m.AuthId equals c.Id
		//				where roleIds.Contains(m.RoleId)
		//				select new CoreAuthorizeModel
		//				{
		//					Name = c.Name,
		//					Method = c.Method
		//				}).ToList();

		//	var hasPermission = data.FirstOrDefault(x => x.Name == name && (x.Method == method || x.Method == string.Empty || x.Method == null));
		//	return hasPermission != null;
		//}
	}
}
