using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboek_Project.Pages.Auth;

public class SearchResult : PageModel
{
    [BindProperty (SupportsGet = true)] 
    public string Search { get; set; }
    public List<Series> Results { get; set; }
    
    public IActionResult OnGet([FromQuery] string Search)
    {
        var res = new SearchRepository();
        Results = res.GetSerie(Search);
        var t = new Auth();
        return t.Check(HttpContext.Session.GetString("authed"));
    }

    public string GetThemeFromList(List<Theme> lst)
    {
        string combinedString = "";
        foreach (var t in lst)
        {
            if (combinedString == "")
            {
                combinedString += t.name;
            }
            else
            {
                combinedString += ", " + t.name;
            }
        }
        return combinedString;
    }
    
    public string GetAuthorFromList(List<Author> lst)
    {
        string combinedString = "";
        foreach (var a in lst)
        {
            if (combinedString == "")
            {
                combinedString += a.Name;
            }
            else
            {
                combinedString += ", " + a.Name;
            }
        }
        return combinedString;
    }

    
}