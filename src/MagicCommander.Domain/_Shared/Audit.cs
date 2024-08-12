namespace MagicCommander.Domain._Shared
{
	public class Audit
	{
        public DateTimeOffset CreatedAt { get; set; } = new();
        public DateTimeOffset UpdatedAt { get; set; } = new();
    }
}
