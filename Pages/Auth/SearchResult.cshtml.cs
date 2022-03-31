using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripboek_Project.Pages.Models;
using Stripboek_Project.Pages.Repositories;

namespace Stripboek_Project.Pages.Auth;

public class SearchResult : PageModel
{
    [BindProperty (SupportsGet = true)] 
    public string Search { get; set; }
    public List<Series> Results { get; set; }
    
    private SearchRepository SearchRepo = new SearchRepository();

    public IActionResult OnGet([FromQuery] string Search)
    {
        Results = SearchRepo.GetSeries(Search);
        Models.Auth auth = new Models.Auth();
        return auth.Check(HttpContext.Session.GetString("authed"));
    }

    // concat functions
    public string GetThemeFromList(List<Theme> list)
    {
        string combinedString = "";
        foreach (Theme t in list) combinedString += combinedString == "" ? t.name : ", " + t.name;
        return combinedString;
    }
    
    public string GetAuthorFromList(List<Author> lst)
    {
        string combinedString = "";
        foreach (Author a in lst) combinedString += combinedString == "" ? a.name : ", " + a.name;
        return combinedString;
    }

    
}