using MagicCommander.Domain._Shared;
using MagicCommander.Domain._Shared.Entities;

namespace MagicCommander.Domain.DecksImports.Entities;

public class DeckImportStatus : Entity, IHasAlternateKey
{
    public Guid Key { get; init; }
    public TypeStatusDeckImport Type { get; init; }
    public string Observation { get; init; }
    public bool Current { get; set; }
    public Audit Audit { get; init; } = new();

    protected DeckImportStatus() { }

    public DeckImportStatus(TypeStatusDeckImport typeStatusDeckImport, string observation = "")
    {
        Key = Guid.NewGuid();
        Type = typeStatusDeckImport;
        Observation = observation;
        Current = true;
    }
}
