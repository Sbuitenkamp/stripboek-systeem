using Dapper;
using Stripboek_Project.Pages.Models;

namespace Stripboek_Project.Pages.Repositories;

public class UsersRepository: Repository
{
    public void AddUser(string username, string password)
    {
        Connect().Query("INSERT INTO User (username, password, role_code) VALUES (@username,@password, @role_code)",new
        {
            username,
            password = BCrypt.Net.BCrypt.HashPassword(password), // encrypt the password for safety n shit
            role_code = 1
        });
    }

    public List<AuthResult> Auth(string username, string password)
    {
        User user = Connect().QuerySingleOrDefault<User>("SELECT * FROM User WHERE username = @username", new { username });
        // return authentication
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.password)) return new List<AuthResult>{ new AuthResult { auth = true, userid = user.id } };
        return new List<AuthResult>{ new AuthResult { auth = false } };
    }
}