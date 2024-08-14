namespace MagicCommander.Domain._Shared.Entities
{
	public class Entity
	{
		public int Id { get; protected set; }

		protected Entity() { }

		protected Entity(int id)
		{
			Id = id;
		}
	}
}
