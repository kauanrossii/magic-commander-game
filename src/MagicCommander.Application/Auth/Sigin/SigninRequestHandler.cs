using MagicCommander.Application._Shared.Dtos.Jwt;
using MagicCommander.Application._Shared.Exceptions;
using MagicCommander.Application._Shared.Helpers;
using MagicCommander.Domain.Users;
using MediatR;

namespace MagicCommander.Application.Auth.Sigin
{
    public class SigninRequestHandler : IRequestHandler<SigninRequest, JwtDto?>
	{
		private readonly IUsersRepository _usersRepository;
		private readonly JwtTokenHelper _jwtTokenHelper;

		public SigninRequestHandler(IUsersRepository usersRepository, JwtTokenHelper jwtTokenHelper)
		{
			_usersRepository = usersRepository;
			_jwtTokenHelper = jwtTokenHelper;
		}

		public async Task<JwtDto?> Handle(SigninRequest request, CancellationToken cancellationToken)
		{
			var existentUser = await _usersRepository
				.FindAsync(user => user.Email == request.Email);

			if (existentUser is null)
				throw new EntityNotFoundException();

			if (request.Password != existentUser.Password)
				throw new EntityNotFoundException();

			var (expiresIn, accessToken) = _jwtTokenHelper
				.GenerateJwtToken(existentUser.Id, existentUser.Role);

			return new JwtDto(
				accessToken,
				expiresIn
			);
		}
	}
}
