namespace MagicCommander.Application._Shared.Dtos
{
	public record Pagination
	{
        public int Page { get; init; }
        public int Quantity { get; init; }
    }
}
