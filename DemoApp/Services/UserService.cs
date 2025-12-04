using DemoApp.Data;

namespace DemoApp.Services;

public class UserService
{
    private readonly Context _context;

    public UserService(Context context)
    {
        _context = context;
    }

    public bool EmailExists(string email)
    {
        return _context.Users.Any(e => e.Email.Value == email);
    }
}