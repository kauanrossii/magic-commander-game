namespace MagicCommander.Application._Shared.Dtos
{
	public record PaginatedResult<TEntity>(
			int Page,
			int Quantity,
			int Total,
			List<TEntity> Result
		);
}
