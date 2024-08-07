namespace User.Models;

public interface IUserFactory
{
    User GetUserById(string id);
    void AddUser(User employee);
    void UpdateUser(User employee);
    void DeleteUser(string id);
    IEnumerable<User> GetUserList();
}