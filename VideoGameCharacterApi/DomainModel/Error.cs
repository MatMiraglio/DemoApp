namespace VideoGameCharacterApi.DomainModel;

public sealed class Error
{
    public string Code { get; }
    public string Message { get; }

    internal Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
}

public static class Errors
{
    public static Error General { get; } = new Error("general", "General");
}