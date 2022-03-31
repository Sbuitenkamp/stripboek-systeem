using Dapper;
using Stripboek_Project.Pages.Auth;
using Stripboek_Project.Pages.Models;

namespace Stripboek_Project.Pages.Repositories;

public class SearchRepository: Repository
{
    public List<Series> GetSeries(string name)
    {
        IEnumerable<Series> comic = Connect().Query<Series, Comic, Author, Theme, Series>("SELECT * FROM Series INNER JOIN Comic ON Series.id = Comic.Series_id INNER JOIN has_a ON Series.id = has_a.Series_id INNER JOIN Author ON has_a.Author_id = Author.id INNER JOIN is_Themed_after ON Series.id = is_Themed_after.Series_id INNER JOIN Theme ON is_Themed_after.Theme_code = Theme.code",(series, comic, author, theme) => {
            series.comics = comic;
            series.authors = new List<Author> { author };
            series.themes = new List<Theme> { theme };
            return series;
        }, splitOn: "isbn,id,code");

        name ??= ""; // if name is null, initialize
        
        IEnumerable<Series> result = comic.Where(c => c.authors.First().name.Contains(name, StringComparison.OrdinalIgnoreCase) || c.comics.title.Contains(name, StringComparison.OrdinalIgnoreCase) || c.title.Contains(name, StringComparison.OrdinalIgnoreCase));
        
        return result.GroupBy(p => p.comics.isbn).Select(g => {
            // serverside grouping and ordering to ease strain on the database
            Series groupedComic = g.First();
            List<Author> lstAuthor = g.Select(p => p.authors.Single()).ToList();
            List<Theme> lstTheme = g.Select(p => p.themes.Single()).ToList();
            
            groupedComic.themes = lstTheme.DistinctBy(t => t.code).ToList();
            groupedComic.authors = lstAuthor.DistinctBy(a => a.id).ToList();
            
            return groupedComic;
        }).ToList();
    }

}