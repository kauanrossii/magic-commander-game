using MagicCommander.Domain._Shared;
using MagicCommander.Domain._Shared.Entities;
using MagicCommander.Domain.Cards.Entities;
using System.Text.Json;

namespace MagicCommander.Domain.DecksImports.Entities;

public class DeckImport : Entity, IHasAlternateKey
{
    private List<DeckImportStatus> _status = new();
    private List<CardImport> _cards = new();

    public Guid Key { get; init; }
    public int UserId { get; init; }
    public Audit Audit { get; init; } = new();
    public IReadOnlyList<CardImport> Cards { get { return _cards; } set { _cards = value.ToList(); } }
    public IReadOnlyList<DeckImportStatus> Status { get { return _status; } set { _status = value.ToList(); } }

    protected DeckImport() { }

    public DeckImport(int userId)
    {
        Key = Guid.NewGuid();
        UserId = userId;
        _status.Add(new DeckImportStatus(TypeStatusDeckImport.Created));
    }

    private bool UpdateStatus(TypeStatusDeckImport typeStatusDeckImport, string observation = "")
    {
        var currentStatus = _status.FirstOrDefault(st => st.Current);

        if (currentStatus?.Type == typeStatusDeckImport)
            return false;

        currentStatus.Current = false;

        _status.Add(new DeckImportStatus(typeStatusDeckImport, observation));
        return true;
    }

    public bool RegisterProcessing()
        => UpdateStatus(TypeStatusDeckImport.Processing);

    public bool RegisterCompleted()
        => UpdateStatus(TypeStatusDeckImport.Completed);

    public bool RegisterError(string observation)
        => UpdateStatus(TypeStatusDeckImport.Error, observation);

    public void AddCardImport(Card card) { }
}
