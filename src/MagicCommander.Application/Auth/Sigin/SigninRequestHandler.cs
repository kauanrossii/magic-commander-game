using MagicCommander.Api.Helpers;
using MagicCommander.Application._Shared.Dtos.Jwt;
using MagicCommander.Application._Shared.Exceptions;
using MagicCommander.Domain.Users;
using MediatR;

namespace MagicCommander.Application.Auth.Sigin
{
    public class SigninRequestHandler : IRequestHandler<SigninRequest, AuthenticationJwtDto?>
	{
		private readonly IUsersRepository _usersRepository;
		private readonly JwtTokenHelper _jwtTokenHelper;

		public SigninRequestHandler(IUsersRepository usersRepository, JwtTokenHelper jwtTokenHelper)
		{
			_usersRepository = usersRepository;
			_jwtTokenHelper = jwtTokenHelper;
		}

		public async Task<AuthenticationJwtDto?> Handle(SigninRequest request, CancellationToken cancellationToken)
		{
			var existentUser = await _usersRepository
				.FindAsync(user => user.Email == request.Email);

			if (existentUser is null)
				throw new EntityNotFoundException();

			if (request.Password != existentUser.Password)
				throw new EntityNotFoundException();

			var result = _jwtTokenHelper
				.GenerateJwtToken(existentUser.Key, existentUser.Role);

			return new AuthenticationJwtDto(
				result.AccessToken,
				result.ExpiresIn
			);
		}
	}
}
