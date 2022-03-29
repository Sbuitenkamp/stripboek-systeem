using Dapper;

namespace Stripboek_Project.Pages.Auth;

public class LanguageRepository: Repository
{
    public List<Language> GetAllanguages()
    {
        return Connect().Query<Language>("SELECT * FROM `Language`;").ToList();
    }

    public void AssignLanguage(int seriesId, string languageCode)
    {
        Connect().Query("INSERT INTO is_written_in (series_id, language_code) VALUES (@seriesId, @languageCode)", new { seriesId, languageCode });
    }
    
    public void UnassignLanguage(int seriesId)
    {
        Connect().Query("DELETE FROM is_written_in WHERE series_id = @seriesId", new { seriesId});
    }
}