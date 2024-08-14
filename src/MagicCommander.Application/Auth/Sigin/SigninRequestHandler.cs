using MagicCommander.Application.Dtos.Jwt;
using MediatR;

namespace MagicCommander.Application.Auth.Sigin
{
	public class SigninRequestHandler : IRequestHandler<SigninRequest, JwtDto?>
	{
		private readonly IUserRepository _userRepository;

		public SigninRequestHandler(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public Task<JwtDto?> Handle(SigninRequest request, CancellationToken cancellationToken)
		{
			// Try to get an user with this email

			// Try verify if the saved hash password is equals to request password

			// Sign jwt with user's informations as claims
		}
	}
}
