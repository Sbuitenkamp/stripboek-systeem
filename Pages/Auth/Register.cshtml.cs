using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripboek_Project.Pages.Models;
using Stripboek_Project.Pages.Repositories;

namespace Stripboek_Project.Pages.Auth;

public class Register : PageModel
{

    public List<Comic> Comic { get; set; }

    public IActionResult OnGet()
    {
        var r = new RegisterRepository();

        Comic = r.GetRegisteredComicsWithSeries();
        Models.Auth auth = new Models.Auth();
        return auth.Check(HttpContext.Session.GetString("authed"));
    }
}