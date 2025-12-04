namespace VideoGameCharacterApi.Services;

public sealed class Error : ValueObject
{
    private const string Separator = "||";

    public string Code { get; }
    public string Message { get; }

    internal Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }

    public string Serialize()
    {
        return $"{Code}{Separator}{Message}";
    }

    public static Error Deserialize(string serialized)
    {
        string[] data = serialized.Split(
            new[] { Separator },
            StringSplitOptions.RemoveEmptyEntries);
        
        return new Error(data[0], data[1]);
    }
}

public static class Errors
{
    public static Error General { get; } = new Error("general", "General");
    public static Error EmailIsTaken { get; } = new Error("email.is.taken", "This email is taken already");
}