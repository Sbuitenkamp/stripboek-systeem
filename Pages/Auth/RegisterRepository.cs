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

    public void AddComic(int isbn, int series_id, string title, string image, bool digital, string description)
    {
        using var connection = Connect();
        var comic = connection
            .Query<Comic>("INSERT INTO comic (isbn, series_id, title, image, description, digital) VALUES (@isbn, @series_id, @title, @image, @description, @digital)",
                new
                {
                    isbn = isbn,
                    series_id = series_id,
                    title = title,
                    image = image,
                    description = description,
                    digital = digital
                });
    }

    public List<Series> GetSeries()
    {
        using var connection = Connect();
        var Series = connection
            .Query<Series>("SELECT * FROM series ");
        return Series.ToList();
    }

    public List<Comic> GetAllComic()
    {
        using var connection = Connect();
        var comic = connection
            .Query<Comic>("SELECT * FROM comic");
        return comic.ToList();
    }

    public void DeleteComic(int isbn)
    {
        using var connection = Connect();
        var comic = connection
            .Query<Comic>("DELETE FROM comic WHERE isbn = @isbn",
                new
                {
                   isbn = isbn 
                });
    }
    
    public void EditComic(int isbn, int series_id, string title, string image, bool digital, string description)
    {
        using var connection = Connect();
        var comic = connection
            .Query<Comic>("UPDATE comic SET isbn=@isbn, series_id=@series_id, title=@title, image=@image, digital=@digital, description=@description WHERE isbn = @isbn",
                new
                {
                    isbn = isbn,
                    series_id = series_id,
                    title = title,
                    image = image,
                    digital = digital,
                    description = description
                });
    }
    public List<Comic> GetSingleComic(int isbn)
    {
        using var connection = Connect();
        var comic = connection
            .Query<Comic>("SELECT * FROM comic WHERE isbn = @isbn", new {isbn = isbn});
        return comic.ToList();
    }

}