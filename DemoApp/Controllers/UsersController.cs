using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using DemoApp.Controllers.Attributes;
using DemoApp.Data;
using DemoApp.DomainModel;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers;

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
    
    [Required]
    [MinLength(4, ErrorMessage = "Name must have at least 4 characters")]
    public string Name { get; set; }
}