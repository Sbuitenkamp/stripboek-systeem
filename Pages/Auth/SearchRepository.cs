using Dapper;

namespace Stripboek_Project.Pages.Auth;

public class SearchRepository: Repository
{
    public List<Comic> GetComic(string name)
    {
        using var connection = Connect();
        var comic = connection
            .Query<Comic>("SELECT * FROM Comic WHERE title LIKE @name", 
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
                "SELECT * FROM Series INNER JOIN Comic ON Series.id = Comic.Series_id INNER JOIN has_a ON Series.id = has_a.Series_id " +
                "INNER JOIN Author ON has_a.Author_id = Author.id INNER JOIN is_Themed_after ON Series.id = is_Themed_after.Series_id " +
                "INNER JOIN Theme ON is_Themed_after.Theme_code = Theme.code WHERE Series.title LIKE @name OR Comic.title LIKE @name OR Author.name LIKE @name",
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