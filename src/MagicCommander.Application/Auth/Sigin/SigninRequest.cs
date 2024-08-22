using MagicCommander.Application._Shared.Dtos.Jwt;
using MediatR;

namespace MagicCommander.Application.Auth.Sigin
{
    public class SigninRequest : IRequest<JwtDto?>
	{
		public string Email { get; init; } = string.Empty;
		public string Password { get; init; } = string.Empty;
    }
}
