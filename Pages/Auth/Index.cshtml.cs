using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboek_Project.Pages.Auth;

public class Index : PageModel
{
    public IActionResult OnGet()
    {
        Models.Auth auth = new Models.Auth();
        return auth.Check(HttpContext.Session.GetString("authed"));
    }
}