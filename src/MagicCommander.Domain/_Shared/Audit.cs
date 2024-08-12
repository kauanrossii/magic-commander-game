namespace MagicCommander.Domain._Shared
{
	public class Audit
	{
        public DateTimeOffset CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTimeOffset UpdatedAt { get; protected set; } = DateTime.UtcNow;

        public void UpdateAudit()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
