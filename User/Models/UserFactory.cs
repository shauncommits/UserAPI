using System.Text.RegularExpressions;

namespace User.Models;

public class UserFactory: IUserFactory
{
    private readonly UserDBContext _dbContext;
    private List<User> _users;
    
    public UserFactory(UserDBContext dbContext)
    {
        _dbContext = dbContext;
        _users = _dbContext.Users.ToList();
    }
    public async Task<User> GetUserById(string id)
    {
        // Using Guid.Parse
        var guid = Guid.Parse(id);
        
        // Using Guid.TryParse
        if (Guid.TryParse(id, out guid))
        {
            // Conversion successful
            return _users.FirstOrDefault(e => e.Id == guid);

        }
        else
        {
            // Conversion failed
            return null;
        }
    }

    public Task AddUser(User user)
    {
        _dbContext.Users.AddAsync(user);
       return _dbContext.SaveChangesAsync();
    }

    public Task UpdateUser(User userUpdate)
    {
        // Updating the user on the database
        var user = _users.FirstOrDefault(e => e.Id == userUpdate.Id);
        
        user.Name = userUpdate.Name;
        user.Surname = userUpdate.Surname;
        user.Role = userUpdate.Role;
        user.Password = userUpdate.Password;
        user.Email = userUpdate.Email;
        
        return _dbContext.SaveChangesAsync();
    }

    public Task DeleteUser(string id)
    {
        // Using Guid.Parse
        var guid = Guid.Parse(id);

        // Using Guid.TryParse
        if (Guid.TryParse(id, out guid))
        {
            // Conversion successful
            
            // Delete Employee from the database
            var user = _users.FirstOrDefault(e => e.Id == guid);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                return _dbContext.SaveChangesAsync();
            }
        }
        else
        {
            // Conversion failed
        }
        

        return null;
    }

    public async Task<List<User>> GetUserList()
    {
        return _users;
    }
}