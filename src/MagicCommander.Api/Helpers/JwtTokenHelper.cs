using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicCommander.Api.Helpers
{
	public class JwtTokenHelper
	{
		private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

		public JwtTokenHelper()
		{
			_jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
		}

		public string GenerateJwtToken(string userId, string role)
		{
			var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtSecret")!);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
				new Claim(ClaimTypes.Role, role)
			};

			var identity = new ClaimsIdentity(claims);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = identity,
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature
				),
				Issuer = Environment.GetEnvironmentVariable("JwtIssuer"),
				Audience = Environment.GetEnvironmentVariable("JwtAudience")
			};

			var token = _jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

			return _jwtSecurityTokenHandler.WriteToken(token);
		}

		public ClaimsPrincipal ValidateJwtToken(string token)
		{
			var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtSecret")!);

			var tokenHandler = new JwtSecurityTokenHandler();

			var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				ValidIssuer = Environment.GetEnvironmentVariable("JwtIssuer"),
				ValidAudience = Environment.GetEnvironmentVariable("JwtAudience"),
				IssuerSigningKey = new SymmetricSecurityKey(key)
			}, out SecurityToken validatedToken);

			return claimsPrincipal;
		}
	}
}
