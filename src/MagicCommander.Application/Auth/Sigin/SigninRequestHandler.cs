using MagicCommander.Application.Dtos.Jwt;
using MagicCommander.Domain.Users;
using MediatR;

namespace MagicCommander.Application.Auth.Sigin
{
	public class SigninRequestHandler : IRequestHandler<SigninRequest, JwtDto?>
	{
		private readonly IUsersRepository _usersRepository;

		public SigninRequestHandler(IUsersRepository userRepository)
		{
			_usersRepository = userRepository;
		}

		public async Task<JwtDto?> Handle(SigninRequest request, CancellationToken cancellationToken)
		{
			// Try to get an user with this email

			// Try verify if the saved hash password is equals to request password

			// Sign jwt with user's informations as claims
			await Task.CompletedTask;
			return new JwtDto();
		}
	}
}
