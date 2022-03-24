using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboek_Project.Pages.Auth;

public class SearchEngine : PageModel
{
    [BindProperty]
    public string Search { get; set; }
    
    public IActionResult OnPostSearch()
    {
        Search = Search;
        return Page();
    }
    
}