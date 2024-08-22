namespace MagicCommander.Domain._Shared.Notifications;

public class Notification
{

	public string Attribute { get; }
    public string ErrorCode { get; }
    public string Message { get; }
    
	public Notification(string attribute, string errorCode, string message)
	{
		Attribute = attribute;
		ErrorCode = errorCode;
		Message = message;
	}
}
