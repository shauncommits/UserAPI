namespace User.Models;

public interface IUserFactory
{
    Task<User> GetUserById(string id);
    Task AddUser(User employee);
    Task UpdateUser(User employee);
    Task DeleteUser(string id);
    Task<List<User>> GetUserList();
}