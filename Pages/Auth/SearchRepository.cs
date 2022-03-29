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
        Console.Write(name);
        using var connection = Connect();
        var comic = connection
            .Query<Series, Comic, Author, Theme, Series>(
                "SELECT * FROM Series INNER JOIN Comic ON Series.id = Comic.Series_id INNER JOIN has_a ON Series.id = has_a.Series_id " +
                "INNER JOIN Author ON has_a.Author_id = Author.id INNER JOIN is_Themed_after ON Series.id = is_Themed_after.Series_id " +
                "INNER JOIN Theme ON is_Themed_after.Theme_code = Theme.code",
                (series, comic, author, theme) =>
                {
                    series.comics = comic;
                    //series.author = author;
                    series.authors = new List<Author>();
                    series.authors.Add(author);
                    //series.theme = theme;
                    series.themes = new List<Theme>();
                    series.themes.Add(theme);
                    return series;
                }, splitOn: "isbn,id,code");

        if (name == null)
        {
            name = "";
        }
        var result = comic.Where(c => c.authors.First().name.Contains(name, StringComparison.OrdinalIgnoreCase) || c.comics.title.Contains(name, StringComparison.OrdinalIgnoreCase) || c.title.Contains(name, StringComparison.OrdinalIgnoreCase));
        result = result.GroupBy(p => p.comics.isbn).Select(g =>
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