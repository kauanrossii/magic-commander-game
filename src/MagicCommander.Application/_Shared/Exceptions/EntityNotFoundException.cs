namespace MagicCommander.Application._Shared.Exceptions
{
	public class EntityNotFoundException : ApplicationException
	{
		public EntityNotFoundException() { }

		public EntityNotFoundException(string message) : base(message) { }
	}
}
