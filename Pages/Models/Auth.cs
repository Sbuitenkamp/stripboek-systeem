using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboek_Project.Pages.Models;

public class Auth : PageModel
{
    public IActionResult Check(string session)
    {
        // if there is no session, return user to the login page
        if (session == null) return Redirect("/");
        return Page();
    }
}