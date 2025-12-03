using VideoGameCharacterApi.DomainModel;
using VideoGameCharacterApi.Migrations;

namespace Tests;

public class EmailTests
{
    [Fact]
    public void Valid_email_should_create_an_email()
    {
        var validEmail = "valid_email@domain.com";
        
        var emailResult = Email.TryCreate(validEmail);
        
        Assert.True(emailResult.IsSuccess);
        Assert.Equal(validEmail, emailResult.Value?.Value);
    }
    
    [Fact]
    public void Equal_emails_should_be_equal()
    {
        var same1 = Email.Create("valid_email@domain.com");
        var same2 = Email.Create("valid_email@domain.com");

        Assert.Equal(same1, same2);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("a@a.a")]
    [InlineData("NoAtSignemail-domain.com")]
    //[InlineData("invaliddomain@domain")]
    public void Invalid_email_should_return_failure(string? invalidEmail)
    {
        var email = Email.TryCreate(invalidEmail);
        
        Assert.True(email.IsFailure);
    }
}