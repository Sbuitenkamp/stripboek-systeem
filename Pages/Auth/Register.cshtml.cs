using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboek_Project.Pages.Auth;

public class Register : PageModel
{

    public List<Comic> comic { get; set; }

    public IActionResult OnGet()
    {
        var r = new RegisterRepository();

        comic = r.Get();
        var t = new Auth();
        return t.Check(HttpContext.Session.GetString("authed"));
    }
}