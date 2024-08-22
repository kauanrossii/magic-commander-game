using System.Text.Json;
using MagicCommander.Domain._Shared.Entities;

namespace MagicCommander.Domain.DecksImports.Entities;

public class CardImport : Entity, IHasAlternateKey
{
    private List<CardImportStatus> _status = new();

    public Guid Key { get; init; }
    public int DeckImportId { get; init; }
    public string Data { get; init; }
    public IReadOnlyList<CardImportStatus> Status { get { return _status; } set { _status = value.ToList(); } }

    protected CardImport() { }

    public CardImport(string data)
    {
        Data = data;
        _status.Add(new CardImportStatus(TypeStatusCardImport.Created));
    }
    
    private bool UpdateStatus(TypeStatusCardImport typeStatusCardImport, string observation = "")
    {
        var currentStatus = _status.FirstOrDefault(st => st.Current);

        if (currentStatus?.Type == typeStatusCardImport)
            return false;

        currentStatus.Current = false;

        _status.Add(new CardImportStatus(typeStatusCardImport, observation));
        return true;
    }

    public bool RegisterInvalid(string observation)
        => UpdateStatus(TypeStatusCardImport.Invalid, observation);

    public bool RegisterError(string observation)
        => UpdateStatus(TypeStatusCardImport.Error, observation);

    public bool RegisterDuplicated(string observation)
        => UpdateStatus(TypeStatusCardImport.Duplicated, observation);

    public bool RegisterAcceptable()
        => UpdateStatus(TypeStatusCardImport.Acceptable);
}
