using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TW.Common
{
	public class CustomAuthorizeRequirement : IAuthorizationRequirement
	{
		public string Method { get; set; }
		public string Name { get; set; }

		public CustomAuthorizeRequirement(string name, string method)
		{
			Method = method;
			Name = name;
		}
	}

	public class CustomAuthorizeHandler : AuthorizationHandler<CustomAuthorizeRequirement>
	{
		readonly IMySessions _sessions;
		public CustomAuthorizeHandler(IMySessions sessions)
		{
			_sessions = sessions;
		}
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizeRequirement requirement)
		{
			if (!context.User.Identity.IsAuthenticated)
			{
				return Task.CompletedTask;
			}

			//if (_sessions.ControlAccess(requirement.Name, requirement.Method))
			//{
			//	context.Succeed(requirement);
			//}

			return Task.CompletedTask;
		}
	}

	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		const string POLICY_PREFIX = "TW.";
		public CustomAuthorizeAttribute(string method = "", string name = "")
		{
			Method = method;
			Name = name;
		}

		public string Name
		{
			get
			{
				return Policy[POLICY_PREFIX.Length..];
			}
			set
			{
				Policy = $"{POLICY_PREFIX}{value}|{Method}";
			}
		}
		public string Method { get; set; }
	}

	internal class CustomAuthorizePolicyProvider : IAuthorizationPolicyProvider
	{
		private DefaultAuthorizationPolicyProvider BackupPolicyProvider { get; }
		public CustomAuthorizePolicyProvider(IOptions<AuthorizationOptions> options)
		{
			BackupPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
		}
		public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());

		public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy>(null);

		public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
		{
			const string POLICY_PREFIX = "TW.";
			if (string.IsNullOrEmpty(policyName) || !policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
			{
				return Task.FromResult<AuthorizationPolicy>(null);
			}

			string value = policyName[POLICY_PREFIX.Length..];
			string[] parts = value.Split('|');

			if (parts.Length < 2 || string.IsNullOrEmpty(parts[0]) || string.IsNullOrEmpty(parts[1]))
			{
				// Trả về null hoặc một policy mặc định nếu các phần không hợp lệ
				return Task.FromResult<AuthorizationPolicy>(null);
			}

			string method = parts[0];
			string name = parts[1];

			var policy = new AuthorizationPolicyBuilder();
			policy.AddRequirements(new CustomAuthorizeRequirement(name, method));
			return Task.FromResult(policy.Build());
		}

	}
}
