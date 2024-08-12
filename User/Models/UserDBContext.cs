using Microsoft.EntityFrameworkCore;

namespace User.Models;

public class UserDBContext: DbContext
{
    public UserDBContext(DbContextOptions<UserDBContext> options): base(options){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(e => e.Id);
    }
    
    public DbSet<User> Users { get; set; }
}