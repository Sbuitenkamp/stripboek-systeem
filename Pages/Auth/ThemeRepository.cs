using Dapper;

namespace Stripboek_Project.Pages.Auth;

public class ThemeRepository: Repository
{
    public List<Theme> GetAllThemes()
    {
        using var connection = Connect();
        var themes = connection.Query<Theme>("SELECT * FROM Theme");
        return themes.ToList();
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