using Dapper;
using Stripboek_Project.Pages.Auth;
using Stripboek_Project.Pages.Models;

namespace Stripboek_Project.Pages.Repositories;

public class RegisterRepository: Repository
{
    public List<Comic> GetRegisteredComicsWithSeries()
    {
        return Connect().Query<Comic, Series, Comic>("SELECT * FROM Comic INNER JOIN series ON Comic.series_id = series.id", (comic, series) => {
            comic.series = new List<Series> { series };
            return comic;
        }, splitOn: "id").ToList();
    }
    
    public List<Comic> GetRegisteredComics()
    {
        return Connect().Query<Comic>("SELECT * FROM Comic").ToList();
    }

    public void AddComic(int isbn, int series_id, string title, string image, bool digital, string description)
    {
        Connect().Query<Comic>("INSERT INTO Comic (isbn, series_id, title, image, description, digital) VALUES (@isbn, @series_id, @title, @image, @description, @digital)", new
        {
            isbn,
            series_id,
            title,
            image,
            description,
            digital
        });
    }

    public List<Series> GetRegisteredSeries()
    {
        return Connect().Query<Series>("SELECT * FROM Series ").ToList();
    }

    public string DeleteComic(int isbn)
    {
        using var connection = Connect();
        var imgPath = connection.Query<Comic>("SELECT image FROM Comic WHERE isbn = @isbn",new { isbn }).First().image; // get the image to return to the handler so it can delete that too
        connection.Query<Comic>("DELETE FROM Comic WHERE isbn = @isbn", new { isbn });
        return imgPath;
    }
    
    public void EditComic(int isbn, int series_id, string title, string image, bool digital, string description)
    {
        Connect().Query<Comic>("UPDATE Comic SET isbn=@isbn, series_id=@series_id, title=@title, image=@image, digital=@digital, description=@description WHERE isbn = @isbn", new
        {
            isbn,
            series_id,
            title,
            image,
            digital,
            description
        });
    }
    public List<Comic> GetSingleComic(int isbn)
    {
        return Connect().Query<Comic>("SELECT * FROM Comic WHERE isbn = @isbn", new {isbn = isbn}).ToList();
    }

}