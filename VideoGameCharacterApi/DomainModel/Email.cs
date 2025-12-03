using System.Text.RegularExpressions;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.DomainModel;

public class Email : ValueObject
{
    private Email(string email)
    {
        Value = email;
    }

    private Email()
    {
        
    }
    
    public string Value { get; private set; }
    
    public static Result<Email> TryCreate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Result<Email>.Fail("empty email");

        string email = input.Trim();

        if (email.Length > 256)
            return Result<Email>.Fail("email is too long");

        if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
            return Result<Email>.Fail("email is invalid");
        
        if (email.Length < 7)
            return Result<Email>.Fail("email is too short");

        return Result<Email>.Ok(new Email(input));
    }

    public static Email Create(string email)
    {
        return TryCreate(email).Value!;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}