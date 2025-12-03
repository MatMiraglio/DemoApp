using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using VideoGameCharacterApi.Controllers.Attributes;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.DomainModel;

namespace VideoGameCharacterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly Context _context;

    public UsersController(Context  context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var users = _context.Users.ToList();
        
        return Ok(users);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(RegisterUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var email = Email.Create(request.Email);
        
        var user =  DomainModel.User.Create(email);
        
        _context.Users.Add(user);
        
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}

public class RegisterUserRequest
{
    [Required]
    [EmailValidation]
    public string Email { get; set; }
}