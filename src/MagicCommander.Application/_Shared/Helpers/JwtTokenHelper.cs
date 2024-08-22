using MagicCommander.Domain.Users.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicCommander.Application._Shared.Helpers
{
	public class JwtTokenHelper
	{
		private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

		public JwtTokenHelper()
		{
			_jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
		}

		public (DateTimeOffset expiresIn, string accessToken) GenerateJwtToken(int userId, TypeRole role)
		{
			var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtSecret")!);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
				new Claim(ClaimTypes.Role, role.ToString())
			};

			var identity = new ClaimsIdentity(claims);
			var expiresIn = DateTime.UtcNow.AddHours(1); 

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = identity,
				Expires = expiresIn,
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature
				),
				Issuer = Environment.GetEnvironmentVariable("JwtIssuer"),
				Audience = Environment.GetEnvironmentVariable("JwtAudience")
			};

			var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

			return (expiresIn, _jwtSecurityTokenHandler.WriteToken(token));
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
