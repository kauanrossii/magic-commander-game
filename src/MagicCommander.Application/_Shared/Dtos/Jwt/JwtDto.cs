namespace MagicCommander.Application._Shared.Dtos.Jwt
{
	public record JwtDto(
		string AccessToken,
		DateTimeOffset ExpiresIn
	);
}
