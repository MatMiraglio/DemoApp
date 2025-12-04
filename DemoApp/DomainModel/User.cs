using DemoApp.Models;

namespace DemoApp.DomainModel;

public class User
{
    public static User Create(Email email)
    {
        return new User { Email =  email };
    }
    
    public int Id { get; private set; }
    
    public Email Email { get; private set; }
    
    public List<ToDo> Todos { get; private set; }
}