using System.Numerics;
using System.Security.Claims;
using System.Security.Principal;

namespace TodoList.Api.Extensions
{
	public static class IdentityExt
	{
		public static string GetUserId(this IPrincipal user)
		{
			if (user == null)
				return string.Empty;

			var identity = (ClaimsIdentity)user.Identity;
			IEnumerable<Claim> claims = identity.Claims;
			return claims.FirstOrDefault(s => s.Type == "UserId")?.Value;
		}
	}
}
