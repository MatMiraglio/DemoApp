using DemoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDosController : ControllerBase
{
    private readonly Context _context;

    public ToDosController(Context  context)
    {
        _context = context;
    }
    
    [HttpGet(Name = "ToDos/mine")]
    public async Task<IActionResult> Get()
    {
        //TODO: get userId from JWT
        const int userId = 1;

        var user = await _context.Users
            .Where(x => x.Id == userId)
            .Select(x => new
            {
                id = x.Id,
                todos = x.Todos.Select(y => new
                {
                    id = y.Id,
                    title = y.Title,
                    name = y.Description
                })
            })
            .SingleAsync();
        
        return Ok(user.todos);
    }
    
    [HttpPost(Name = "PostToDo")]
    public IActionResult Post()
    {
        //TODO: get userId from JWT
        const int userId = 1;
        
        _context.Users.Where(x => x.Id == userId);
        
        return Ok();
    }
}