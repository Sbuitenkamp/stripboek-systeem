using System.Data;
using MySql.Data.MySqlClient;

namespace Stripboek_Project.Pages.Repositories;

public abstract class Repository
{
    // create connection to db for a repository
    protected IDbConnection Connect()
    {
        return new MySqlConnection(
            "Server=127.0.0.1;Port=3306;" +
            "Database=Comics;" +
            "Uid=Sbuitenkamp;Pwd=pass123;"
        );
    }
}