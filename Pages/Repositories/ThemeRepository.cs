using Dapper;
using Stripboek_Project.Pages.Auth;
using Stripboek_Project.Pages.Models;

namespace Stripboek_Project.Pages.Repositories;

public class ThemeRepository: Repository
{
    public List<Theme> GetAllThemes()
    {
        return Connect().Query<Theme>("SELECT * FROM Theme").ToList();
    }

    public void AssignTheme(List<ThemedAfter> data)
    {
        using var connection = Connect();
        // loop through the list and create relations using the relation table
        foreach (ThemedAfter themeAssign in data) connection.Query("INSERT INTO is_themed_after (series_id, theme_code) VALUES (@series_id, @theme_code)", new { themeAssign.series_id, themeAssign.theme_code });
    }

    public void UnassignTheme(int seriesId)
    {
        Connect().Query("DELETE FROM is_themed_after WHERE series_id = @series_id", new { series_id = seriesId });
    }
}