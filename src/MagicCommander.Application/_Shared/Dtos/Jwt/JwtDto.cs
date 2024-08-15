namespace MagicCommander.Application._Shared.Dtos.Jwt
{
	public record AuthenticationJwtDto(
		string AccessToken,
		DateTimeOffset ExpiresIn
	);
}
