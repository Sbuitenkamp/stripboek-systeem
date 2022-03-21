using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboek_Project.Pages.Auth;

public class Auth : PageModel
{
    public IActionResult Check(string session)
    {
        if (session == null)
        {
            return Redirect("/");
        }
        else
        {
            return Page();
        }
    }
}