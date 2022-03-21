using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboek_Project.Pages.Auth;

public class Index : PageModel
{
    public IActionResult OnGet()
    {
        var t = new Auth();
        return t.Check(HttpContext.Session.GetString("authed"));
    }
}