using MagicCommander.Application.Dtos.Jwt;
using MediatR;

namespace MagicCommander.Application.Auth.Sigin
{
	public class SigninRequest : IRequest<JwtDto?>
	{
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
    }
}
