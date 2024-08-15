using MagicCommander.Domain._Shared;
using MagicCommander.Domain._Shared.Entities;

namespace MagicCommander.Domain.DecksImports.Entities;

public class CardImportStatus : Entity, IHasAlternateKey
{
    public Guid Key { get; init; }
    public TypeStatusCardImport Type { get; init; }
    public string Observation { get; init; }
    public bool Current { get; set; }
    public Audit Audit { get; init; } = new();

    protected CardImportStatus() { }

    public CardImportStatus(TypeStatusCardImport typeStatusCardImport, string observation = "")
    {
        Key = Guid.NewGuid();
        Type = typeStatusCardImport;
        Observation = observation;
        Current = true;
    }
}
