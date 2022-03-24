using System.Data;
using System.Net;
using Dapper;
using MySql.Data.MySqlClient;

namespace Stripboek_Project.Pages.Auth;

public class SearchRepository
{
    private IDbConnection Connect()
    {
        return new MySqlConnection(
            "Server=127.0.0.1;Port=3306;" +
            "Database=comics;" +
            "Uid=root;Pwd=;"
        );
    }

    public List<Comic> GetComic(string name)
    {
        using var connection = Connect();
        var comic = connection
            .Query<Comic>("SELECT * FROM comic WHERE title LIKE @name", 
        new
            {
                name = "%" + @name + "%"
            });
        return comic.ToList();
    }
    public List<Series> GetSerie(string name)
    {
        using var connection = Connect();
        var comic = connection
            .Query<Series, Comic, Author, Theme, Series>(
                "SELECT * FROM series INNER JOIN comic ON series.id = comic.series_id INNER JOIN has_a ON series.id = has_a.series_id " +
                "INNER JOIN author ON has_a.author_id = author.id INNER JOIN is_themed_after ON series.id = is_themed_after.series_id " +
                "INNER JOIN theme ON is_themed_after.theme_code = theme.code WHERE series.title LIKE @name OR comic.title LIKE @name OR author.name LIKE @name",
                (series, comic, author, theme) =>
                {
                    series.comics = comic;
                    series.authors = new List<Author>();
                    series.authors.Add(author);
                    series.themes = new List<Theme>();
                    series.themes.Add(theme);
                    return series;
                }, splitOn: "isbn,id,code",
            param :new
                {
                    name = "%" + @name + "%"
                });
        var result = comic.GroupBy(p => p.comics.isbn).Select(g =>
        {
            var groupedComic = g.First();
            var lstAuthor = g.Select(p => p.authors.Single()).ToList();
            var lstTheme = g.Select(p => p.themes.Single()).ToList();
            groupedComic.themes = lstTheme.DistinctBy(t => t.code).ToList();
            groupedComic.authors = lstAuthor.DistinctBy(a => a.id).ToList();
            return groupedComic;
        });
        return result.ToList();
    }

}