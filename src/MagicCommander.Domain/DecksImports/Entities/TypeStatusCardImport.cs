namespace MagicCommander.Domain.DecksImports.Entities;

public enum TypeStatusCardImport : int
{
    Invalid = -2,
    Duplicated = -1,
    Error = 0,
    Created = 1,
    Acceptable = 2
}
