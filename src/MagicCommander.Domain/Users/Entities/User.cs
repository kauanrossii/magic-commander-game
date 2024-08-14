using MagicCommander.Domain._Shared;
using MagicCommander.Domain._Shared.Entities;

namespace MagicCommander.Domain.Users.Entites
{
	public class User : Entity, IHasAlternateKey
	{
        public Guid Key { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
		public Audit Audit{ get; set; } = new();

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
