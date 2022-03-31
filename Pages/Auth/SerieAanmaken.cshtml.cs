using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripboek_Project.Pages.Models;
using Stripboek_Project.Pages.Repositories;

namespace Stripboek_Project.Pages.Auth;

public class SerieAanmaken : PageModel
{
    public List<Series> Series { get; set; }
    public List<Theme> Themes { get; set; }
    public List<Author> Authors { get; set; }
    public List<Language> Languages { get; set; }
    private SerieRepository SerieRepo { get; } = new SerieRepository();
    private ThemeRepository ThemeRepo { get; } = new ThemeRepository();
    private AuthorRepository AuthorRepo { get; } = new AuthorRepository();
    private LanguageRepository LanguageRepo { get; } = new LanguageRepository();
    
    public IActionResult OnGet()
    {
        Series = SerieRepo.GetAllSeries();
        Themes = ThemeRepo.GetAllThemes();
        Authors = AuthorRepo.GetAllAuthors();
        Languages = LanguageRepo.GetAllLanguages();
        Models.Auth auth = new Models.Auth();
        return auth.Check(HttpContext.Session.GetString("authed"));
    }

    public IActionResult OnPostSerieAanmaken(string serieTitle, int[] author, string serieDate, string serieDescription, int serieAmount, string[] themeCode, string languageCode)
    {
        int newId = SerieRepo.AddSeries(serieTitle, serieDate, serieDescription, serieAmount);
        List<ThemedAfter> themeAssigns = new List<ThemedAfter>();
        List<HasA> authorAssigns = new List<HasA>();
        foreach (string code in themeCode) themeAssigns.Add(new ThemedAfter { series_id = newId, theme_code = code });
        foreach (int id in author) authorAssigns.Add(new HasA { series_id = newId, author_id = id });
        ThemeRepo.AssignTheme(themeAssigns);
        LanguageRepo.AssignLanguage(newId, languageCode);
        AuthorRepo.AssignAuthor(authorAssigns);
        Series = SerieRepo.GetAllSeries();
        return Redirect("/auth/serieaanmaken");
    }

    public IActionResult OnPostDeleteSerie(int serieIdDelete)
    {
        // remove the foreign keys before removing the serie
        ThemeRepo.UnassignTheme(serieIdDelete);
        LanguageRepo.UnassignLanguage(serieIdDelete);
        AuthorRepo.UnassignAuthor(serieIdDelete);
        SerieRepo.DeleteSeries(serieIdDelete);
        Series = SerieRepo.GetAllSeries();
        return Redirect("/auth/serieaanmaken");
    }

    public IActionResult OnGetAlleSeries(int id)
    {
        return new JsonResult(SerieRepo.GetSingleSeries(id));
    }

    public IActionResult OnPostEditSerie(int id, string title, string year, string description, int completeAmount)
    {
        SerieRepo.EditSeries(id, title, year, description, completeAmount);
        return Redirect("/auth/serieaanmaken");
    }
}