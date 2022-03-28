using Dapper;

namespace Stripboek_Project.Pages;

public class UsersRepository: Repository
{
    public List<User> Get()
    {
        using var connection = Connect();
        var users = connection
            .Query<User>("SELECT * FROM User");
        return users.ToList();
    }

    public void AddUser(string username, string password)
    {
        using var connection = Connect();
        var users = connection
            .Query("INSERT INTO User (username, password, role_code) VALUES (@username,@password, @role_code)",
                new
                {
                    username = username,
                    password = BCrypt.Net.BCrypt.HashPassword(password),
                    role_code = 1
                });
        return;
    }

    public List<authresult> Auth(string username, string password)
    {
        using var connection = Connect();
        var user = connection
            .QuerySingleOrDefault("SELECT * FROM User WHERE username = @username",
                new
                {
                    username = username
                });
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.password))
        {
            return new List<authresult>{new authresult { auth = true, userid = user.id}};
        }
        else
        {
            return new List<authresult>{new authresult { auth = false}};
        }
    }
}

public class authresult
{
    public bool auth { get; set; }
    public int userid { get; set; }
}