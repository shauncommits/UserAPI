using Microsoft.EntityFrameworkCore;

namespace User.Models;

public class UserDBContext: DbContext
{
    public UserDBContext(DbContextOptions<UserDBContext> options): base(options){}
    
    public DbSet<User> User { get; set; }
}