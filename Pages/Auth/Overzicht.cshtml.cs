using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripboek_Project.Pages.Models;
using Stripboek_Project.Pages.Repositories;

namespace Stripboek_Project.Pages.Auth;

public class Overzicht : PageModel
{
    public List<Collection> Collection { get; set; }
    public IActionResult OnGet(int id)
    {
        Collection = new CollectionRepository().GetCollection(id);
        Models.Auth auth = new Models.Auth();
        return auth.Check(HttpContext.Session.GetString("authed"));
    }
}