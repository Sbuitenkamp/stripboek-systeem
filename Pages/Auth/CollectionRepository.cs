using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace Stripboek_Project.Pages.Auth;

public class CollectionRepository
{
    private IDbConnection Connect()
    {
        return new MySqlConnection(
            "Server=127.0.0.1;Port=3306;" +
            "Database=comics;" +
            "Uid=root;Pwd=;"
        );
    }
    
    public List<Collection> Get(int id)
    {
        using var connection = Connect();
        var collections = connection
            .Query<Collection, Comic, User, Series, Collection>("SELECT * FROM collectioncomic "+
            "INNER JOIN comic ON collectioncomic.isbn = comic.isbn "+
            "INNER JOIN user ON collectioncomic.user_id = user.id "+
            "INNER JOIN series ON comic.series_id = series.id "+
            "WHERE collectioncomic.user_id = @id",
                (collection, comic, user, series) =>
                {
                    collection.comics = new List<Comic>();
                    collection.comics.Add(comic);
                    collection.user = user;
                    comic.series = new List<Series>();
                    comic.series.Add(series);
                    return collection;
                }, splitOn: "isbn, isbn, id, id",
                param: new
                {
                    id = id
                });
        return collections.ToList();
    }
    
    
}