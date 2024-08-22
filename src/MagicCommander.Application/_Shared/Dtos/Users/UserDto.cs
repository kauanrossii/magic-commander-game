using MagicCommander.Domain.Users.Entites;

namespace MagicCommander.Application._Shared.Dtos.Users
{
	public record UserDto
	{
        public string Name { get; protected set; } = string.Empty;
        public string Email { get; protected set; } = string.Empty;

        public static UserDto FromEntity(User user)
        {
            return new UserDto
            {
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}
