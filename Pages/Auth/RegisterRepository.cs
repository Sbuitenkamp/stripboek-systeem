using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Stripboek_Project.Pages.Auth;

public class RegisterRepository
{
    private IDbConnection Connect()
    {
        return new MySqlConnection(
            "Server=127.0.0.1;Port=3306;" +
            "Database=comics;" +
            "Uid=root;Pwd=;"
        );
    }
    
    public List<Comic> Get()
    {
        using var connection = Connect();
        var collections = connection
            .Query<Comic, Series, Comic>("SELECT * FROM comic INNER JOIN series ON comic.series_id = series.id",
                (comic, series) =>
                {
                    comic.series = new List<Series>();
                    comic.series.Add(series);
                    return comic;
                }, splitOn: "id");
        return collections.ToList();
    }
}