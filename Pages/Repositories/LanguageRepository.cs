using Dapper;
using Stripboek_Project.Pages.Auth;
using Stripboek_Project.Pages.Models;

namespace Stripboek_Project.Pages.Repositories;

public class LanguageRepository: Repository
{
    public List<Language> GetAllLanguages()
    {
        return Connect().Query<Language>("SELECT * FROM `Language`;").ToList();
    }

    public void AssignLanguage(int seriesId, string languageCode)
    {
        // create relations using the connection table is_written_in
        Connect().Query("INSERT INTO is_written_in (series_id, language_code) VALUES (@seriesId, @languageCode)", new { seriesId, languageCode });
    }
    
    public void UnassignLanguage(int seriesId)
    {
        // delete relations
        Connect().Query("DELETE FROM is_written_in WHERE series_id = @seriesId", new { seriesId});
    }
}