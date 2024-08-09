namespace MagicCommander.Domain.User
{
	public class User
	{
        public int Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

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
