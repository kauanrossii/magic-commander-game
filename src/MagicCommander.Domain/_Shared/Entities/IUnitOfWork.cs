namespace MagicCommander.Domain._Shared.Entities;

public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
