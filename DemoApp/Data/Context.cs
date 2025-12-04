using DemoApp.DomainModel;
using DemoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity => entity.OwnsOne(c => c.Email, email =>
            {
                email.Property(c => c.Value).HasColumnName("Email").IsRequired();
            })
        );
    }

    
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<ToDo> ToDos => Set<ToDo>();
    public DbSet<User> Users => Set<User>();
}
