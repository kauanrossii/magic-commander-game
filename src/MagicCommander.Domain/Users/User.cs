namespace MagicCommander.Domain.Users
{
	public class User
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

		protected User() { }

		public User(string name, string password, string email)
		{
			Key = Guid.NewGuid();
			Name = name;
			Password = password;
			Email = email;
		}
	}
}
