namespace MagicCommander.Domain._Shared.Exceptions;

public class MagicApiResourceNotFoundException : ApplicationException
{
    public MagicApiResourceNotFoundException(string message) 
        : base(message) { }
}
