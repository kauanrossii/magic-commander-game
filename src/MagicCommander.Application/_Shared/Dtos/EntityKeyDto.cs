namespace MagicCommander.Application._Shared.Dtos;

public class EntityKeyDto
{
    public Guid Key { get; init; }

    public EntityKeyDto(Guid key) {
        Key = key;
    }
}
