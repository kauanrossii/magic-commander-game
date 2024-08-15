using MagicCommander.Domain._Shared;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain.Users.Entities;

namespace MagicCommander.Domain.Users.Entites
{
	public class User : Entity, IHasAlternateKey
	{
		public Guid Key { get; init; }
		public string Name { get; protected set; } = string.Empty;
		public string Password { get; protected set; } = string.Empty;
		public string Email { get; protected set; } = string.Empty;
		public TypeRole Role { get; protected set; } = TypeRole.Common;
		public Audit Audit { get; protected set; } = new();

		protected User() { }

		public User(string name, string email, string password)
		{
			Key = Guid.NewGuid();
			Name = name;
			Email = email;
			Password = password;
		}
	}
}
