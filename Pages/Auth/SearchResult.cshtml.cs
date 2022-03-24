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
        return Page();
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
                combinedString += a.name;
            }
            else
            {
                combinedString += ", " + a.name;
            }
        }
        return combinedString;
    }

    
}