using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Data.Configurations;
using VideoGameCharacterApi.DomainModel;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Data;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity => entity.OwnsOne(c => c.Email, email =>
            {
                email.Property(c => c.Value).HasColumnName("Email").IsRequired();
            })
        );


        // .Property(x => x.Email).HasConversion(  
        //     x => x.Value, 
        //     value => Email.Create(value));
    }

    
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<ToDo> ToDos => Set<ToDo>();
    public DbSet<User> Users => Set<User>();
}
