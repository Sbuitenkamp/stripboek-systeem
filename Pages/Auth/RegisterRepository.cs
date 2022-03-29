using Dapper;

namespace Stripboek_Project.Pages.Auth;

public class RegisterRepository: Repository
{
    public List<Comic> Get()
    {
        using var connection = Connect();
        var collections = connection
            .Query<Comic, Series, Comic>("SELECT * FROM Comic INNER JOIN series ON Comic.series_id = series.id",
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
            .Query<Comic>("INSERT INTO Comic (isbn, series_id, title, image, description, digital) VALUES (@isbn, @series_id, @title, @image, @description, @digital)",
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
            .Query<Series>("SELECT * FROM Series ");
        return Series.ToList();
    }

    public List<Comic> GetAllComic()
    {
        using var connection = Connect();
        var comic = connection
            .Query<Comic>("SELECT * FROM Comic");
        return comic.ToList();
    }

    public string DeleteComic(int isbn)
    {
        using var connection = Connect();
        var imgPath = connection.Query<Comic>("SELECT image FROM Comic WHERE isbn = @isbn",new { isbn = isbn }).First().image; // get the image to return to the handler
        var comic = connection
            .Query<Comic>("DELETE FROM Comic WHERE isbn = @isbn",
                new
                {
                   isbn = isbn 
                });
        return imgPath;
    }
    
    public void EditComic(int isbn, int series_id, string title, string image, bool digital, string description)
    {
        using var connection = Connect();
        var comic = connection
            .Query<Comic>("UPDATE Comic SET isbn=@isbn, series_id=@series_id, title=@title, image=@image, digital=@digital, description=@description WHERE isbn = @isbn",
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
            .Query<Comic>("SELECT * FROM Comic WHERE isbn = @isbn", new {isbn = isbn});
        return comic.ToList();
    }

}