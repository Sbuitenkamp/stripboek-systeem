using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Stripboek_Project.Pages;

public class UsersRepository
{
    private IDbConnection Connect()
    {
        return new MySqlConnection(
            "Server=127.0.0.1;Port=3306;" +
            "Database=comics;" +
            "Uid=root;Pwd=;"
        );
    }
    
    public List<User> Get()
    {
        using var connection = Connect();
        var users = connection
            .Query<User>("SELECT * FROM user");
        return users.ToList();
    }

    public void AddUser(string username, string password)
    {
        using var connection = Connect();
        var users = connection
            .Query("INSERT INTO user (username, password, role_code) VALUES (@username,@password, @role_code)",
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
            .QuerySingleOrDefault("SELECT * FROM user WHERE username = @username",
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